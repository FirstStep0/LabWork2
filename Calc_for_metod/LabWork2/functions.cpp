#include <algorithm>
#include "functions.h"

double k(double x) {
    if (x <= 0.5) return pow(x + 1, 2);
    else return pow(x, 2);
}

double rev_k(double x) {
    return 1.0 / k(x);
}

double q(double x) {
    if (x <= 0.5) return pow(x + 1, 2);
    else return pow(x, 2);
}

double f(double x) {
    if (x <= 0.5) return exp(-x + 0.5);
    else return exp(x - 0.5);
}

double test_k(double x) {
    if (x <= 0.5) return 2.25;
    else return 0.25;
}

double rev_test_k(double x) {
    return 1.0 / test_k(x);
}

double test_q(double x) {
    if (x <= 0.5) return 1.0;
    else return 1.0;
}

double test_f(double x) {
    if (x <= 0.5) return 0.0;
    else return 1.0;
}

//u(x) = C1*exp(2x/3) + C2*exp(-2x/3) x <= ksi
//u(x) = C3*exp(2x) + C4*exp(-2x)  ksi < x
//u(0)=u0
//u(1)=u1
//u(ksi-0)=u(ksi+0)
//w(ksi-0)=w(ksi+0)

void get_coeff(double u0, double u1, double& C1, double& C2, double& C3, double& C4) {
    C1 = ((5.3890560989306502272304275) * u0 + 2.0602675685227495424255444 + 3.7936678946831777353963044 * u1) / 32.2251542481757626061992188;
    C2 = ((26.8360981492451123789687913) * u0 - 2.0602675685227495424255444 - 3.7936678946831777353963044 * u1) / 32.2251542481757626061992188;
    C3 = ((13.3072619293991039690150315) * u1 - 8.8856608678170901840564997 - 4.1868372752582685858843760 * u0) / 87.5970512121059811588486671;
    C4 = (-59.6402030890001319807290966 - (29.1700282930111945722647612) * u1 + 84.0948746835784023359147097 * u0) / 32.2251542481757626061992188;
}

double Co1, Co2, Co3, Co4;

double test_function(double x) {
    if (x <= 0.5) return Co1 * exp(2 * x / 3) + Co2 * exp(-2 * x / 3);
    else return Co3 * exp(2 * x) + Co4 * exp(-2 * x) + 1;
}

void set_coeff(double& C1, double& C2, double& C3, double& C4) {
    Co1 = C1;
    Co2 = C2;
    Co3 = C3;
    Co4 = C4;
}