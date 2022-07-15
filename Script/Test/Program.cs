using System;
using System.Text;
namespace ConsoleApp1
{
    class Program
    {
        #region  年历表练习
        /// <summary>
        /// 根据年月日，计算星期数的方法
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="day">天</param>
        /// <returns>星期数</returns>
        private static int GetWeekByDay(int year, int month, int day)
        {
            DateTime dt = new DateTime(year, month, day);
            return (int)dt.DayOfWeek;
        }

        private static bool IsLeap(int year)
        {
            return (year % 4 == 0 && year % 100 != 0 || year % 400 == 0);
        }

        private static int GetDaysMonth(int year, int month)
        {
            if (month < 1 || month > 12)
                return 0;
            switch (month)
            {
                case 2:
                    return IsLeap(year) ? 29 : 28;
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                default:
                    return 31;
            }

        }

        private static void PrintMonth(int year, int month)
        {
            Console.WriteLine("{0}年{1}月", year, month);
            Console.WriteLine("日\t一\t二\t三\t四\t五\t六");
            int week = GetWeekByDay(year, month, 1);//显示空白
            for (int i = 0; i < week; i++)
                Console.Write("\t");
            int days = GetDaysMonth(year, month);//显示日
            for (int i = 1; i <= days; i++)
            {
                Console.Write(i + "\t");
                if (GetWeekByDay(year, month, i) == 6)//每周六换行
                    Console.WriteLine();
            }

        }

        private static void PrintYear(int year)
        {
            for (int i = 1; i <= 12; i++)
            {
                PrintMonth(year, i);
                Console.WriteLine();
                Console.WriteLine();
            }


        }
        #endregion
        #region  买彩票练习
        /*
         * 1.玩法说明：
         *   双色球投注区分为红球号码区和蓝球号码区，红球号码范围为01~33，
         * 蓝球号码范围为01~16。双色球每期从33个红球中开出6个号码，从16个
         * 蓝球中开出1个号码作为中奖号码，双色球玩法即是竞猜开奖号码的6个
         * 红球号码和1个蓝球号码顺序不限。
         * 
         * 2.中奖规则：
         * （1）一等奖：红球、蓝球全中，奖金1亿元。
         * （2）二等奖：红球全中，奖金200万元。
         * （3）三等奖：红球中5个，蓝球中1个，奖金3000元。
         * （4）四等奖：红球中4个，蓝球中1个，奖金200元。
         * （5）五等奖：红球中3个，蓝球中1个，奖金10元。
         * （6）六等奖：蓝球中1个，奖金5元。
         */
        private static int[] BuyTicket()
        {
            int[] ticket = new int[7];
            for(int i = 0; i < 6; )
            {
                Console.WriteLine("请输入第{0}个红球代码：",i+1);
                int red = int.Parse(Console.ReadLine());
                if (red < 1 || red > 33)
                    Console.WriteLine("购买的号码超过范围！");
                else if (Array.IndexOf(ticket, red) >= 0)
                    Console.WriteLine("该号码已存在！");
                else ticket[i++] = red;
            }
            while(true)
            {
                Console.WriteLine("请输入蓝球代码");
                int blue = int.Parse(Console.ReadLine());
                if (blue >= 1 && blue <= 16)
                {
                    ticket[6] = blue;
                    break;
                }
                else Console.WriteLine("购买的号码超过范围！");
            }
            return ticket;
        }

        static Random random = new Random();
        private static int[] CreatRandom()
        {
            int []ticket=new int[7];
            for(int i = 0; i < 6;)
            {
                int red = random.Next(1, 34);
                if (Array.IndexOf(ticket, red) < 0) 
                ticket[i++] = red;
            }
            ticket[6] = random.Next(1, 17);
            Array.Sort(ticket,0,6);
            return ticket;
        }

        private static int Compare(int[] myticket, int[] randomticket)
        {
            int blueCount = myticket[6] == randomticket[6] ? 1 : 0;
            int redCount=0;
            for (int i = 0; i < 6; i++)
                if (Array.IndexOf(randomticket, myticket[i], 0, 6) >= 0)
                    redCount++;
            if (blueCount + redCount == 7)
                return 1;
            else if (redCount == 6)
                return 2;
            else if (redCount + blueCount == 6)
                return 3;
            else if (redCount + blueCount == 5)
                return 4;
            else if (redCount + blueCount == 4)
                return 5;
            else if (blueCount == 1)
                return 6;
            else return 0;

        }
        #endregion
        #region  2048游戏
        //去零数组
        static int [] RemoveZero(int []a)
        {
            int []array= new int[a.Length];
            for(int i = 0, j = 0; i < a.Length; i++)
            {
                if (a[i] != 0)
                    array[j++] = a[i];
            }
            return array;
        }
        //合并数组
        private static int [] Merge(int[] a)
        {
            int []array=RemoveZero(a);
            for(int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] != 0 && array[i] == array[i + 1])
                {
                    array[i] *= 2;
                    array[i + 1] = 0;
                }

            }
            array = RemoveZero(array);
            return array;

        }
        //上移操作
        private static int [,] Up(int[,]a)
        {
            int[] array = new int[a.GetLength(0)];
            for(int i = 0; i < a.GetLength(1); i++)
            {
                for (int j = 0; j < a.GetLength(0); j++)
                    array[j] = a[j, i];
                array = Merge(array);
                for (int j = 0; j < a.GetLength(0); j++)
                    a[j, i] = array[j];
            }
            return a;
        }
        //下移操作
        private static int[,] Down(int[,] a)
        {
            int[] array = new int[a.GetLength(0)];
            for (int i = 0; i < a.GetLength(1); i++)
            {
                for (int j = a.GetLength(0)-1; j >= 0; j--)
                    array[a.GetLength(0) - 1 - j] = a[j, i];
                array = Merge(array);
                for (int j = a.GetLength(0)-1; j >= 0; j--)
                    a[j, i] = array[a.GetLength(0) - 1 - j];
            }
            return a;
        }
        //左移操作
        private static int[,] Left(int[,] a)
        {
            int[] array = new int[a.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                    array[j] = a[i, j];
                array = Merge(array);
                for (int j = 0; j < a.GetLength(1); j++)
                    a[i, j] = array[j];
            }
            return a;
        }
        //右移操作
        private static int[,] Right(int[,] a)
        {
            int[] array = new int[a.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = a.GetLength(1) - 1; j >= 0; j--)
                    array[a.GetLength(1) - 1 - j] = a[i, j];
                array = Merge(array);
                for (int j = a.GetLength(1) - 1; j >= 0; j--)
                    a[i, j] = array[a.GetLength(1) - 1 - j];
            }
            return a;
        }
        //打印数组
        private static void Print(Array array)
        {
            for(int i = 0; i < array.GetLength(0); i++)
            {
                for(int j=0;j<array.GetLength(1);j++)
                    Console.Write(array.GetValue(i,j)+"\t");
                Console.WriteLine();
            }
        }

        //创建二维数组
        private static int [,] Create()
        {
            int [,]array={
                {2,2,4,8 },
                {2,4,4,4 },
                {0,8,4,0 },
                {2,4,0,4 }
            };
            return array;
        }
        //
        #endregion
        #region  参数数组
        private static int Add(params int[] arr)
        {
            int sum = 0;
            foreach (var item in arr)
                sum += item;
            return sum;
        }
        #endregion
        #region  传递引用参数
        private static void Fun(ref int a)
        {
            a = 2;
        }
        #endregion
        static void Main(string[] args)
        {

            #region  年历表
            //Console.WriteLine("请输入年份：");
            //PrintYear(int.Parse(Console.ReadLine()));
            #endregion
            #region  双色球彩票
            /*
            int count = 0;
            do
            {
                Console.Clear();
                count += 10;
                int[] myTicket = BuyTicket();
                int[] randomTicket = CreatRandom();
                int level = Compare(myTicket, randomTicket);
                if (level == 0)
                    Console.WriteLine("很遗憾，你未中奖！总共消费{0}元。",count);
                else Console.WriteLine("恭喜你获得{0}等奖！总共消费{1}元。", level,count);
                Console.WriteLine("是否继续购买（输入0则退出）？");
            } while (int.Parse(Console.ReadLine()) != 0);
            Console.ReadLine();
            */
            #endregion
            #region  2048
            Console.WriteLine("原数组为：");
            Print(Create());
            Console.WriteLine("上移：");
            Print(Up(Create()));
            Console.WriteLine("下移：");
            Print(Down(Create()));
            Console.WriteLine("左移：");
            Print(Left(Create()));
            Console.WriteLine("右移：");
            Print(Right(Create()));
            Console.ReadLine();
            #endregion
            #region  简单输入输出/赋值
            Console.Title = "从零开始";
            Console.WriteLine("你好世界！");
            Console.ReadLine();
            string name = "AK47";
            int room = 5;
            float sun = 0.8f;
            #endregion
            #region 占位符、格式化
            string str = string.Format("枪的名称为{0}，容量为{1:d2}，金额为{2:c}，损耗度为{3:p0}。",name,room,200,sun);//货币¥10.00
            Console.WriteLine(str);
            //转义符：\" \' \0
            Console.WriteLine("今天\"today\"!");
            //源代码（.cs文本文件）->CLS编译->CIL（通用中间语言）（exe dll）->CLR编译->机器码0 1
            //             CLS：公共语言规范(目的：跨语言)        CLR：公共语言运行库（目的：优化 跨平台）
            #endregion
            #region  数据类型转换
            //1.Parse转换：string转换为其他数据类型（待转数据必须“像”该数据类型）
            string number = "18.20";
            //int num1 = int.Parse(number);(报错）
            float num2 = float.Parse(number);//0会被自动去除
            Console.WriteLine(num2);
            /*拓展：
            TryParse方法：主要用于判断用户输入能否成功转换，返回两个结果：
            1.out：转换后的结果
            2.返回值：是否可以转换（bool）
            int result;
            bool re = int.TryParse("250+",out result);
            */
            //2.任意类型转string
            string str1 = num2.ToString();
            Console.WriteLine(str1);
            //注意：由多种类型变量参与运算，结果自动向较大的类型提升
            //快捷运算符不做自动类型提升（a+=3与a=a+3的区别，其中a为byte类型)
            //推断类型（var）：根据所赋数据，推断类型
            var v1 = 1;//int
            var v2 = "1";//string
            var v3 = 2.0;//double
            Console.WriteLine(v1+v2+v3);
            //任意数组类型(多态）：
            //声明 父类类型  赋值  子类对象
            Array arr01 = new int[2] ;
            Array arr02 = new string[3];
            //object 万类之祖，任意类型
            //练习：四位数相加
            string str2 = "0125";
            int result=0;
            for (int i = 0; i < 4; i++)
                result += int.Parse(str2[i].ToString());
            Console.WriteLine(result);
            #endregion
            #region  字符串常用方法
            string str3 = "今天星期几";
            str3 = str3.Insert(5, "？");//插入字符
            Console.WriteLine(str3);
            Console.WriteLine(str3.IndexOf('天'));//查找索引(从0开始）
            Console.WriteLine(str3.Remove(2));//删除2以后的字符
            Console.WriteLine(str3.Replace("几？", "二。"));//替换成“二。”
            Console.WriteLine(str3.StartsWith("今天"));//开头是否匹配
            Console.WriteLine(str3.Contains("星期六"));//是否存在该子串
            #endregion
            #region  数组
            int [] array01= { 1, 2, 3 };
            int[,] array1 = new int[5, 3];//二维数组定义
            /*
               数组遍历字典（从头到尾依次读取数组元素）：
               foreach（元素类型 变量名 in 数组名称）
               {
                    变量名 即数组中每一个元素
               }
             */
            foreach(int item in array01)
                Console.WriteLine(item);
            //交错数组(不规则的表格，每个元素都为一维数组，类似于列数参差不齐的二维数组）
            //创建具有2个元素的交错数组
            int[][] array2 = new int[2][];
            //创建一维数组，赋值给交错数组的第一个元素
            array2[0] = new int[3];
            array2[1] = new int[2];
            //循环输出交错数组：
            /*
            foreach(int []array in array2)
            {
                foreach(int element in array)
                    Console.WriteLine(element);
            }
            for(int i = 0; i < array2.Length; i++)
            {
                for(int j=0;j<array2[i].Length;j++)
                    Console.WriteLine(array2[i][j]);
            }
            */
            //参数数组 params
            //对方法内部：只是简单的一维数组
            //对方法外部（调用者）：1.可以传递数组。2.可以传递一组数据类型相同的变量集合。3.可以不传递参数
            //作用：简化调用者调用方法的代码。
            Console.WriteLine(Add(1, 2, 3, 4, 5));
            Console.WriteLine(Add(array01));
            #endregion
            #region  数据类型
            /*
              1.值类型（结构（数值类型、bool、char）+枚举）  2.引用类型（接口+类（string、Array、委托））
              内存分配：值类型和引用类型的声明都在栈中，而值类型数据存储在栈中，引用类型数据存储在堆中
              注意将数组跟字符串区分开，string是直接对地址进行修改（object也一样<string为object的基类>），
              而数组只是修改地址下的数据。
            */
            //三种参数：值参数、引用参数、输出参数
            //ref引用参数：传递引用参数地址（ref a相当于C++中的&a）
            int a = 1;
            Fun(ref a);//a变为2
                       //out输出参数：与引用参数类似（用于接收方法的结果）
                       //Fun(out a);//a变为2
            /*
              引用参数与输出参数区别：
            1.方法内部必须为输出参数赋值
            2.输出参数传递之前可以不赋值
            3.引用参数作用：改变数据  输出参数作用：返回结果  值参数：传递信息
            */

            //垃圾回收器GC（Garbage Collection）：从栈的引用开始跟踪，回收无法跟踪到的那一块堆内存。比较消耗CPU（尽量减少垃圾）。

            //拆装箱：
            int a01 = 1;
            object o01 = a01;//装箱操作
            //其中object o01在堆中开辟了三块内存，分别存储int、同步块索引、类型对象指针
            //先开辟三块地，把a的1保存在新开辟的int中，接着将o01指向int的1（即装箱操作）
            //“比较”消耗性能
            int b01 = (int)o01;//拆箱操作

            /*装箱 box：值类型隐式转换成object类型或由此值类型实现的任何接口类型的过程。（“比较”消耗性能）
            内部机制：
            1、在堆中开辟内存空间
            2、将值类型的数据复制到堆中
            3、返回堆中新分配对象的地址
            */

            /*拆箱 unbox：从object类型到值类型或从接口类型到实现该接口的值类型的显示转换（“比较”消耗性能但消耗性能比装箱时小）
            内部机制：
            1.判断给定类型是否为装箱时的类型
            2.返回已装箱实例中属于原值类型字段的地址
            */
            int num = 100;
            string str01 = num.ToString();//没装箱
            string str02 = num + "";//装箱
                                    //形参为object类型，实参传递值类型，则装箱。（可以通过重载、泛型避免拆装箱）
            #endregion
            #region  string字符串
            /*两个特性：
            1.字符串池：在创建字符串变量前先查找是否有相同字符串文本，如果存在则直接返回该对象的引用，
            若不存在则开辟空间存储（目的：提高内存利用率）。
            2.不可变性：字符串常量一旦进入内存，就不得再次改变。每次修改都是重新开辟空间存储新字符串，替换栈中引用。
            （防止因内存空间不一致导致破环别的内存空间，导致内存泄漏。<与object同理>）
            */

            //可变“字符串”：String Builder（一个类）（有增删改查功能）
            StringBuilder builder = new StringBuilder(10);//一次开辟可以容纳10个字符大小的空间
            //(若实际超出则会开辟一个更大的空间，将原本的数据拷贝过去，接着把引用替换掉，原来的成为垃圾）
            //优点：可以在原有空间修改字符串,避免产生垃圾（前提是不自动扩容）
            //适用性：频繁对字符串操作
            for (int i = 0; i < 5; i++)
                builder.Append(i);
            #endregion
            #region  二维数组助手
            string[,] array02 = new string[3, 4];
            for(int i = 0; i < array02.GetLength(0); i++)
            {
                for (int j = 0; j < array02.GetLength(1); j++)
                {
                    array02[i, j] = i.ToString() + j.ToString();
                    Console.Write(array02[i,j]+"  ");
                }
                Console.WriteLine();
            }
            string[] result01 = DoubleArrayHelper.GetElements(array02, 2, 0, Direction.Right, 5);
            for (int i = 0; i < result01.Length; i++)
                Console.WriteLine("**" + result01[i] + "--");
            #endregion
            Console.ReadLine();
        }

    }
}
