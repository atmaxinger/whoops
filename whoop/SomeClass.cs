using System.Collections.Generic;

namespace whoop
{
    class WorkOrder
    {
        [PropertyType("36da77e6-e05f-435c-8c81-1c52ff0b791f")]
        public string Name { get; set; }
        
        [PropertyType("851689dc-b452-4bbe-a94c-eaa9df300b72")]
        public List<string> People { get; set; }
        
        [PropertyType("ec5daf23-172e-4baf-8668-96d8d190ad8e")]
        public int Age { get; set; }
    }
}