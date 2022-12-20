#include <iostream>
#include <fstream>
#include <vector>
#include <cstdlib>
#include <cmath>
#include "functions.h"
using namespace std;

const int precision = 10;

struct info {
    double x, u, v, db_v;
    int n;
    double error;
    double accuracy;
};

struct reference {
    int n = -1;
    double max_error = -1;
    double x_max_error = -1;
    double max_accuracy = -1;
    double x_max_accuracy = -1;
};

void update_ref(reference& ref, double error, double x_error, double acc, double x_acc) {
    error = abs(error);
    acc = abs(acc);
    if (error > ref.max_error) {
        ref.max_error = error;
        ref.x_max_error = x_error;
    }

    if (acc > ref.max_accuracy) {
        ref.max_accuracy = acc;
        ref.x_max_accuracy = x_acc;
    }
}

struct global_data {
    double a, b;
    int n;
    double u0;
    double u1;
    double break_point;
    double(*rev_k)(double);
    double(*q)(double);
    double(*f)(double);
    double (*integral)(double(*func)(double), double, double);
};

double integral_middle_rectangles(double(*func)(double), double a, double b) {
    double I = (b - a) * func(a + (b - a) / 2);
    return I;
}

/*double* Tridiagonal_matrix_algorithm(int n, double* A, double* B, double* C, double* fi, double m1, double m2, double hi1, double hi2)  //A(n-1) B(n-1); C(n-1), D(n-1);  
{
    double* X = new double[n];
    double* alpha = new double[n - 1];
    double* beta = new double[n - 1];

    alpha[0] = hi1;
    beta[0] = m1;

    for (int i = 1; i < n - 1; i++)
    {
        double temp = C[i - 1] - alpha[i - 1] * A[i - 1];
        alpha[i] = B[i - 1] / temp;
        beta[i] = (fi[i - 1] + A[i - 1] * beta[i - 1]) / temp;
    }


    X[n - 1] = (m2 + hi2 * beta[n - 2]) / (1 + hi2 * alpha[n - 2]);

    for (int i = n - 2; i >= 0; i--)
    {
        X[i] = alpha[i - 1] * X[i + 1] + beta[i - 1];
    }

    delete[] alpha;
    delete[] beta;

    return X;
}*/

double* Tridiagonal_matrix_algorithm(int n, double* A, double* B, double* C, double* D)  //A, B(n-1); C, D(n);  
{
    double* X = new double[n];
    double* alpha = new double[n - 1];
    double* beta = new double[n - 1];

    alpha[0] = B[0] / C[0];
    beta[0] = D[0] / C[0];

    for (int i = 1; i < n - 1; i++)
    {
        double temp = C[i] - A[i - 1] * alpha[i - 1];
        alpha[i] = B[i] / temp;
        beta[i] = (D[i] - A[i - 1] * beta[i - 1]) / temp;
    }

    X[n - 1] = (D[n - 1] - A[n - 2] * beta[n - 2]) / (C[n - 1] - A[n - 2] * alpha[n - 2]);

    for (int i = n - 2; i >= 0; i--)
    {
        X[i] = beta[i] - alpha[i] * X[i + 1];
    }

    delete[] alpha;
    delete[] beta;

    return X;
}


vector<info> solve_task(const global_data& data) {

    double (*integral)(double(*func)(double), double, double) = data.integral;
    double(*rev_k)(double) = data.rev_k;
    double(*q)(double) = data.q;
    double(*f)(double) = data.f;
    double u0 = data.u0;
    double u1 = data.u1;

    vector<info> records;

    double eps = data.break_point;
    int bp_pos = 0;

    double a = data.a;
    double b = data.b;
    int n = data.n;

    double* _a = new double[n + 1];
    double* _fi = new double[n];
    double* _d = new double[n];

    double h = (double)(b - a) / n;
    double lx, x;

    //fill array _a
    bp_pos = 0;
    lx = a;
    x = lx + h;
    for (int i = 1; i <= n; ++i) {

        //if (/*bp_pos < break_points.size() && lx <= break_points[bp_pos] && break_points[bp_pos] <= x*/) {
        if (lx <= eps && eps <= x) {
            _a[i] = h / (integral(rev_k, lx, eps) + integral(rev_k, eps, x));
        }
        else {
            _a[i] = h / integral(rev_k, lx, x);
        }

        lx = x;
        x += h;

       // while (lx >= break_points[bp_pos] && (bp_pos + 1) < break_points.size()) bp_pos++;
    };

    lx = h / 2;
    x = lx + h;
    for (int i = 1; i <= n - 1; ++i) {

        //возможно попадание нескольких точек разрыва на один отрезок (в общем случае)
        //if (bp_pos < break_points.size() && lx <= break_points[bp_pos] && break_points[bp_pos] <= x) {
        if (lx <= eps && eps <= x) {
            _fi[i] = (integral(f, lx, eps) + integral(f, eps, x)) / h;
            _d[i] = (integral(q, lx, eps) + integral(q, eps, x)) / h;
            //top++;
        }
        else {
            _fi[i] = integral(f, lx, x) / h;
            _d[i] = integral(q, lx, x) / h;
            //top++;
        }

        lx = x;
        x += h;

       // while (lx >= break_points[bp_pos] && (bp_pos + 1) < break_points.size()) bp_pos++;
    };

    double* A = new double[n]; //1,..,n-1
    double* B = new double[n]; //1,..,n-1
    double* C = new double[n + 1]; //
    double* D = new double[n + 1];

    for (int i = 1; i <= n - 1; ++i) {
        A[i] = _a[i] / (h * h);
        B[i] = _a[i + 1] / (h * h);
        C[i] = (_a[i + 1] + _a[i]) / (h * h) + _d[i];
    }

    //parse
    C[0] = 1;
    for (int i = 1; i <= n - 1; ++i) {
        C[i] = -C[i];
    }
    C[n] = 1;

    B[0] = 0;

    for (int i = 0; i <= n - 2; ++i) {
        A[i] = A[i + 1];
    }
    A[n - 1] = 0;


    D[0] = u0;
    for (int i = 1; i <= n - 1; ++i) {
        D[i] = -_fi[i];
    }
    D[n] = u1;

    double* X = Tridiagonal_matrix_algorithm(n + 1, A, B, C, D);

    x = a;
    for (int i = 0; i <= n; ++i) {
        info rec;
        rec.n = i;
        rec.x = x;
        rec.v = X[i];
        records.push_back(rec);
        x += h;
    }

    delete[] _a;
    delete[] _fi;
    delete[] _d;

    delete[] A;
    delete[] B;
    delete[] C;
    delete[] D;
    
    return records;
}

vector<info> solve(const global_data& in_data, reference& ref, double (*func)(double)) {
    global_data data = in_data;
    ref.n = data.n;
    vector<info> v1 = solve_task(data);
    //cout << "size:\n";
    //cout << v1.size() << "\n";
    data.n *= 2;
    vector<info> v2 = solve_task(data);
   // cout << v2.size() << "\n";
    const int si = v1.size();
    for (int i = 0; i < si; ++i) {
        v1[i].db_v = v2[i * 2].v;
        v1[i].u = func(v1[i].x);

        v1[i].error = v1[i].u - v1[i].v;
        v1[i].accuracy = v1[i].v - v1[i].db_v;

        update_ref(ref, v1[i].error, v1[i].x, v1[i].accuracy, v1[i].x);
    }
    return v1;
}

int parce_input_and_choose_task(ifstream& in, global_data& data) {
    int task_num;
    int n;
    double u0, u1;
    try {
        in >> task_num;
        in >> u0 >> u1;
        in >> n;
    }
    catch (...) {
        cout << "incorrect input";
    }
    
    data.a = 0;
    data.b = 1;
    data.n = n;
    data.u0 = u0;
    data.u1 = u1;
    data.break_point = 0.5;
    data.integral = integral_middle_rectangles;
    switch (task_num) {
    case 0:
        data.rev_k = rev_test_k;
        data.q = test_q;
        data.f = test_f;
        break;
    case 1:
        data.rev_k = rev_k;
        data.q = q;
        data.f = f;
        break;
    }
    return task_num;
};

void print_reference(const reference& ref, bool isTest) {
    cout << "REFERENCE\n";
    if (isTest) {
        cout << ref.n << "\n";
        cout << ref.max_error << "\n";
        cout << ref.x_max_error << "\n";
    }
    else {
        cout << ref.n << "\n";
        cout << ref.max_accuracy << "\n";
        cout << ref.x_max_accuracy << "\n";
    }
    cout << "END_REFERENCE\n";
}

int main(int argc, char* argv[])
{
    cout.precision(precision);

    if (argc < 2)
    {
        std::cerr << "No input file\n";
        exit(-1);
    }

    ifstream in_file(argv[1]);

    global_data data;
    reference ref;
    int task_num;
    bool isTest;
    if (in_file.is_open())
    {
        task_num = parce_input_and_choose_task(in_file, data);
    }
    else
    {
        std::cerr << "Could not open file\n";
        exit(-2);
    }
    in_file.close();

    isTest = (task_num == 0);
    double Co1, Co2, Co3, Co4;
    get_coeff(data.u0, data.u1, Co1, Co2, Co3, Co4);
    set_coeff(Co1, Co2, Co3, Co4);

    vector<info> v = solve(data, ref, test_function);

    print_reference(ref, isTest);
    
    if(isTest){
        int si = v.size();
        cout << "n\tx\tu\tv\tu - v\n";
        for (int i = 0; i < si; ++i) {
            cout << v[i].n << "\t" << v[i].x << "\t" << v[i].u << "\t" << v[i].v << "\t" << v[i].error << "\n";
        }
    }
    else {
        int si = v.size();
        cout<<"n\tx\tv\tv'\tv - v'\n";
        for (int i = 0; i < si; ++i) {
            cout << v[i].n << "\t" << v[i].x << "\t" << v[i].v << "\t" << v[i].db_v << "\t" << v[i].accuracy << "\n";
        }
    }
}
