using System;
using System.Reflection;
using System.Collections.Generic;

namespace homework1
{
    class MyClass { int i = 0; }
    struct MyStruct { char a; double b;  }
    class Program
    {
        public static void ShowInfo(Type t)
        {
            if (t == null) { Console.WriteLine("Wrong type"); return; }

            string[] fieldNames = new string[t.GetFields().Length];
            for (int i = 0; i < fieldNames.Length; i++)
            {
                fieldNames[i] = t.GetFields()[i].Name;
            }
            string sFieldNames = String.Join(", ", fieldNames);
            int nProp = t.GetProperties().Length;
            string[] propList = new string[nProp];
            for (int i = 0; i < nProp; i++)
            {
                propList[i] = t.GetProperties()[i].Name;
            }
            string sPropList = String.Join(", ", propList);
            Console.WriteLine("Информация по типу: " + 
                t.Namespace + '.' + t.Name + "\n\n" +
                "   Значимый тип:           " + t.IsValueType + '\n' +
                "   Пространство имён:      " + t.Namespace + '\n' +
                "   Сборка:                 " + t.Assembly.GetName().Name + '\n' +
                "   Общее число элементов:  " + (t.GetMethods().Length + t.GetFields().Length) + '\n' +
                "   Число методов:          " + t.GetMethods().Length + '\n' +
                "   Число свойств:          " + t.GetProperties().Length + '\n' +
                "   Число полей:            " + t.GetFields().Length + '\n' +
                "   Список полей:           " + sFieldNames + '\n' +
                "   Список свойств:         " + sPropList + '\n' +
                "Нажмите " + 'M' + " для вывода дополнительной информации по" +
                " методам :" + '\n' +
                "Нажмите " + '0' + " для выхода в главное меню.\n"
                );
            switch (char.ToLower(Console.ReadKey(true).KeyChar))
            {
                case 'm':
                    {
                        Console.WriteLine("Методы типа " + t.Namespace + '.' + t.Name + "\n\n" +
                            "Название           Число перегрузок            Число параметров" + '\n');
                        string[] fM = new string[t.GetMethods().Length];
                        Dictionary<string, int[]> memDic = new Dictionary<string, int[]>();
                        int min_met = t.GetMethods()[0].GetParameters().Length;
                        int max_met = t.GetMethods()[0].GetParameters().Length;
                        for (int i = 0; i < fM.Length; i++)
                        {
                            int[] tmp = new int[3];
                            fM[i] = t.GetMethods()[i].Name;
                            if (memDic.ContainsKey(fM[i]))
                            {
                                memDic[fM[i]][0] += 1;
                            }
                            else
                            {
                                tmp[0] = 1;
                                if (t.GetMethods()[i].GetParameters().Length < min_met)
                                {
                                    min_met = t.GetMethods()[i].GetParameters().Length;
                                    tmp[1] = t.GetMethods()[i].GetParameters().Length;
                                }
                                if (t.GetMethods()[i].GetParameters().Length > max_met)
                                {
                                    max_met = t.GetMethods()[i].GetParameters().Length;
                                    tmp[2] = t.GetMethods()[i].GetParameters().Length;
                                }
                                memDic.Add(fM[i], tmp);
                            }
                        }
                        foreach (var i in memDic)
                        {
                            Console.WriteLine(i.Key + "\t\t" + i.Value[0] + "\t" + i.Value[1] + ".." + i.Value[2]);
                        }
                        Console.WriteLine("\n\n");
                        return;
                    }
                case '0': return;
                default:
                    {
                        Console.WriteLine("Неверный выбор! Возврат в главное меню");
                        return;
                    }
            }
        }

        public static Type SelectType()
        {
            Console.WriteLine(
                "Выберете тип из списка:\n" +
                    "1 – uint\n" +
                    "2 – int\n" +
                    "3 – long\n" +
                    "4 – float\n" +
                    "5 – double\n" +
                    "6 – char\n" +
                    "7 - string\n" +
                    "8 – MyClass\n" +
                    "9 – MyStruct\n" +
                    "0 – Выход в главное меню\n");
            Type t = typeof(uint);
            switch (char.ToLower(Console.ReadKey(true).KeyChar))
            {
                //case '0': ;
                case '1': t = typeof(uint); break;
                case '2': t = typeof(int); break;
                case '3': t = typeof(long); break;
                case '4': t = typeof(float); break;
                case '5': t = typeof(double); break;
                case '6': t = typeof(char); break;
                case '7': t = typeof(string); break;
                case '8': t = typeof(MyClass); break;
                case '9': t = typeof(MyStruct); break;
            }
            return t;
        }

        public static void ChangeConsoleView()
        {
            Console.Clear();
            Random rnd = new Random();
            int color = rnd.Next(1, 16);
            int x = rnd.Next(0, 64);
            Console.BackgroundColor = (ConsoleColor)color;
            Console.WindowHeight = 88 - x;
            Console.WindowWidth = 100 + x;
        }

        public static void ShowAllTypeInfo()
        {
            Assembly[] refAsm = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> types = new List<Type>();
            foreach (Assembly asm in refAsm)
            {
                types.AddRange(asm.GetTypes());
            }

            int classcounter = 0;
            int valuecounter = 0;
            int interfacecounter = 0;

            //с наибольшим количеством методов
            string max_name = "";
            int max_len = 0;

            //самое длинное имя
            string long_name = types[0].Name;
            int long_name_i = types[0].Name.Length;

            //с наибольшим количеством параметров
            int max_param = types[0].GetMethods()[0].GetParameters().Length;
            string max_param_name = types[0].GetMethods()[0].Name;

            for (int i = 0; i < types.Count; i++)
            {
                if (types[i].IsClass) classcounter++;
                else if (types[i].IsValueType) valuecounter++;
                else if (types[i].IsInterface) interfacecounter++;

                if (types[i].GetMethods().Length > max_len)
                {
                    max_name = types[i].Name;   //наибольшее количество методов
                    max_len = types[i].GetMethods().Length;
                }

                for (int j = 0; j < types[i].GetMethods().Length; j++)
                {
                    if (types[i].GetMethods()[j].Name.Length > long_name_i)
                    {
                        long_name = types[i].GetMethods()[j].Name;  //самое длинное имя
                        long_name_i = types[i].GetMethods()[j].Name.Length;
                    }

                    if (types[i].GetMethods()[j].GetParameters().Length > max_param)
                    {
                        max_param_name = types[i].GetMethods()[j].Name; //наибольшее количество параметров
                        max_param = types[i].GetMethods()[j].GetParameters().Length;
                    }
                }
            }


            Console.WriteLine("Общая информация по типам:\n" +
                "Подключённые сборки:\t\t\t\t" + refAsm.Length + '\n' +
                "Всего типов по всем подключённым сборкам:\t" + types.Count + '\n' +
                "Ссылочные типы:\t\t\t\t\t" + classcounter + '\n' +
                "Значимые типы:\t\t\t\t\t" + valuecounter + '\n' +
                "Типы-интерфейсы:\t\t\t\t" + interfacecounter + '\n' +
                "Тип с максимальным числом методов:\t\t" + max_name + '(' + max_len + ')' + '\n' +
                "Самое длинное название метода:\t\t" + long_name + '\n' +
                "Метод с наибольшим числом аргументов:\t\t" + max_param_name + '\n'
                );
            return;
        }

        public static Type InsertTypeName ()
        {
            Type t = null;
            Console.WriteLine("Введите имя типа (без System.)");
            string name = Console.ReadLine();

            //Assembly myAsm = Assembly.GetExecutingAssembly();
            //t = myAsm.GetType(name);

            Assembly[] refAsm = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asm in refAsm)
            {
                t = asm.GetType("System." + name);
                if (t != null) return t;
            }
            return null;
        }

        public static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("Информация по типам: \n\n");
                Console.WriteLine(
                    "1 - Общая информация по типам\n" +
                    "2 - Выбрать из списка\n" +
                    "3 - Ввести имя типа\n" +
                    "4 - Параметры консоли\n" +
                    "0 - Выход из программы\n");
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '0': { return; }
                    case '1': { ShowAllTypeInfo(); break; }
                    case '2': { ShowInfo(SelectType());  break; }
                    case '3':
                        {
                            ShowInfo(InsertTypeName());
                            break;
                        }
                    case '4': { ChangeConsoleView();  break; }
                    default: { Console.WriteLine("Неверный выбор!"); break; }
                }
            }
        }

        static void Main(string[] args)
        {
            //            Console.WindowHeight = 120;
            //            Console.WindowWidth = 100;
                        Type t = typeof(int);
                MainMenu();
        }
    }
}
