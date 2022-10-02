using System.Globalization;
using System.Net.NetworkInformation;

namespace getPing
{
    class getPing
    {

        static void Main()
        {
            Console.WriteLine("Выберите: \n1 - для ввода подсети \n2 - для ввода диапазона");
            string? numb = Console.ReadLine();
            string[] subNet = new string[3];
            int n1, n2;
            

            switch (numb)
            {
                case "1":
                    Console.WriteLine("Введите подсеть");
                    subNet[0] = Console.ReadLine();
                    n1 = subNet[0].LastIndexOf('.');
                    subNet[0] = subNet[0].Substring(0,n1);
                    string[,] result1 = new string[255, 2];
                    Parallel.For(1, 256, i =>
                    {
                        result1[i - 1, 0] = subNet[0] + "." + i;
                        result1[i - 1, 1] = Answer(subNet[0] + "." + i);
                        
                        //Console.WriteLine(subNet[0] +"." + i + " " + Answer(subNet[0]+ "." + i));

                    });
                    Show(result1);
                    break;
                case "2":
                    Console.WriteLine("Введите начальный IP диапазона");
                    subNet[0] = Console.ReadLine();
                    subNet[1] = subNet[0];
                    n1 = subNet[0].LastIndexOf('.');
                    subNet[0] = subNet[0].Substring(0, n1); //первые три октета диапазона
                    subNet[1] = subNet[1].Substring(n1 + 1); //последний октет начального IP диапазона
                    Console.WriteLine("Введите конечный IP диапазона");
                    subNet[2] = Console.ReadLine();
                    n2 = subNet[2].LastIndexOf('.');
                    subNet[2] = subNet[2].Substring(n2+1); //последний октет конечного IP диапазона
                    string[,] result2 = new string[int.Parse(subNet[2]) - int.Parse(subNet[1]) + 1, 2];
                    Parallel.For(int.Parse(subNet[1]), int.Parse(subNet[2]) + 1, i =>
                    {
                        result2[i - 1, 0] = subNet[0] + "." + i;
                        result2[i - 1, 1] = Answer(subNet[0] + "." + i);
                        //String.Format(subNet[0] + "." + i + " " + Answer(subNet[0] + "." + i));

                    });
                    Show(result2);
                    break;

                default:
                    Console.WriteLine("Вы ввели неверное значение");
                    break;
             


            }
            


        }

        static string Answer(string address)
        {
            Ping p = new Ping();
            PingReply answer = p.Send(address);
            //Console.WriteLine(answer.Status.ToString());
            if (string.Compare(answer.Status.ToString(), "DestinationHostUnreachable") == 0 || string.Compare(answer.Status.ToString(), "TimedOut") == 0)
                return "No";
            else
                return "Yes";

        }
        static void Show(string[,] massiv)
        {
            for (int i = 0; i < massiv.GetLength(0); i++)
            {
                for (int j = 0; j < massiv.GetLength(1); j++)
                    Console.Write("\t{0}", massiv[i, j]);

                Console.WriteLine();
            }
        }


    }

}

