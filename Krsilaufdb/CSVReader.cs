using System.ComponentModel;
using System.Reflection;
using System.Reflection.PortableExecutable;

namespace Kreislauf 
{
    public class CSVReader<T> where T : new ()
    {
        Type type;
        StreamReader reader;
        public CSVReader()
        {
            type = typeof(T);
            reader = new StreamReader(type.Name + ".csv");
        }
        public CSVReader(string path)
        {
            type = typeof(T);
            reader = new StreamReader(path);
        }
   

        private T Creater(object[] vals)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            object inc = Activator.CreateInstance(type, vals);
            return (T)Convert.ChangeType(inc, typeof(T));

        }
        private ConstructorInfo GetConstructor(string[] header)
        {
            ConstructorInfo[] constructor = type.GetConstructors();
            foreach (ConstructorInfo cons in constructor)
            {
                string[] names = new string[header.Length];
                int i = 0;
                foreach(ParameterInfo name in cons.GetParameters())
                {
                    if(cons.GetParameters().ToList().Count != header.ToList().Count)
                    {
                        break;
                    }
                    names[i++] = name.Name.ToLower(); 
                }
                if(Enumerable.SequenceEqual(names, header))
                {
                    return cons;
                }
                
               
                
            }
            return null;
            
        }
        private int[] HeaderOrder(ConstructorInfo cons, string[] header)
        {
            int[] order = new int[0];
       
            
                int i = 0;
                order = new int[cons.GetParameters().Length];
            while (true)
            {
                i = 0;
                foreach (ParameterInfo pi in cons.GetParameters())
                {
                    order[i++] = header.ToList().FindIndex(x => x == pi.Name);// OG: name;alter;beruf == 0;1;2 Alt: alter;name;beruf == 1;0;2 || Info String;Int;String 


                }
                if (!order.Contains(-1))
                {
                    break;
                }
            }
            return order;
            
           
        }
        private Type[] ParameterTypes(ConstructorInfo con)
        {
            List<Type> types = new List<Type>();
            foreach (ParameterInfo pi in con.GetParameters())
            {
                types.Add(pi.ParameterType);
            }
            return types.ToArray();
        }
        public List<T> Read()
        {

            List<T> list = new List<T>();
            bool header = true;
            int[] order = new int[0];
            ConstructorInfo con = null;
            while (!reader.EndOfStream)
            {
                
                if (header)
                {
                    List<string> line = reader.ReadLine().ToLower().Split(';').ToList();
                    con = GetConstructor(line.ToArray());
                    order = HeaderOrder(con ,line.ToArray());
                }
                else
                {
                    List<string> line = reader.ReadLine().Split(';').ToList();
                    var save = new object[order.Length];
                    Type[] types = ParameterTypes(con);
                    foreach (int i in order)
                    {
                        var data = TypeDescriptor.GetConverter(types[i]).ConvertFromString(line[i]);
                        
                        save[i] = data;
                    }
                    list.Add(Creater(save));
                }
                header = false;
            }
            return list;
        }
        public string[] Readheader()
        {
            return reader.ReadLine().ToLower().Split(';');
        }
        public List<T> Read(Dictionary<string,int> alt) // Vorname, 5
        {

            List<T> list = new List<T>();
          
            ConstructorInfo con = GetConstructor(alt.Keys.ToArray());
         
            while (!reader.EndOfStream)
            { 
                    List<string> line = reader.ReadLine().Split(';').ToList();
                    var save = new List<object>();
                ParameterInfo[] parameterInfos = con.GetParameters();
                    
                    foreach (ParameterInfo para in parameterInfos)
                    {
                    var data = new object();
                    if (alt[para.Name] != -1)
                    {
                        data = line[alt[para.Name]];
                    }
                    else
                    {
                        data = "null";
                    }
                        

                        save.Add(data);
                    }
                list.Add(Creater(save.ToArray()));
            }
            return list;
        }



    }
}
