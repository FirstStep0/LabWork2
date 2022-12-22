import matplotlib.pyplot as plt
import os.path
from sys import exit 
from sys import argv

if (not(os.path.exists("./tmp/result.txt"))):  
    print("The file does not exist")
    exit()

with open("./tmp/result.txt") as res:
    all_result = [row.strip() for row in res]

n_task = int(argv[1])

if(n_task == 0):
    name_graph = "Аналитическое и численное решения\nтестовой задачи"
    name_u = "u - Точное решение"
    name_v = "v - Численное решение" 
    name_OY = "Значения u и v"

    name_error = "Погрешность решения тестовой задачи"
    name_E = "Погрешность численного решения"
    name_OY_E = "Значение |u - v|"
if(n_task == 1):
    name_graph = "Численные решения на сетке и двойной сетке\nосновной задачи"
    name_u = "v - Численное решение на сетке"
    name_v = "v' - Численное решение на двойной сетке"
    name_OY = "Значения v и v'"

    name_error = "Точность решения основной задачи"
    name_E = "Точность численного решения"
    name_OY_E = "Значение |v - v'|"


result = []
i = 0
for row in all_result:
    if(i > 5):
        result.append(row.split())
        #print(row)
    i += 1

x = []
u = []
v = []
E = []
for i, row in enumerate(result):
    x.append(float(row[1]))
    u.append(float(row[2]))
    v.append(float(row[3]))
    E.append(float(row[4]))

result.pop()

fig = plt.figure(figsize = (12, 7))

if(True):
    fig1 = plt.figure(1, figsize=(12, 7))
    (x_v) = fig1.subplots(1)
    fig1.suptitle(name_graph, fontsize=16, fontweight='bold')
    x_v.plot(x, u,'bo--', label = name_u, lw = 2)
    x_v.plot(x, v, 'go--', label = name_v, lw = 2)
    x_v.set_xlabel('Значение x', fontsize = 15)
    x_v.set_ylabel(name_OY, fontsize = 15)
    x_v.legend(fontsize = 15)
    x_v.grid()

    fig2 = plt.figure(2, figsize=(12, 7));
    (x_E) = fig2.subplots(1)
    fig2.suptitle(name_error, fontsize=16, fontweight='bold')
    x_E.plot(x, E, "o-", color = 'yellow', label = name_E, lw = 2)
    x_E.set_xlabel('Значение x', fontsize = 15)
    x_E.set_ylabel(name_OY_E, fontsize = 15)
    x_E.legend(fontsize = 15)
    x_E.grid()
plt.show()
