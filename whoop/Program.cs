using System;
using System.Linq.Expressions;

namespace whoop
{
    class Program
    {
        static String Translate<T>(Expression<Func<T, bool>> expression)
        {
            var vis = new MyTranslator();
            vis.Visit(expression);
            return vis.Query;
        }
        
        static void Main(string[] args)
        {
            Expression<Func<WorkOrder, bool>> exp = wo => wo.Age == 12 && (wo.Name == "Mark" || wo.People.Contains("Mary"));
            Console.WriteLine(Translate(exp));
        }
    }
}