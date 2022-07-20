using System;

namespace RecordsRest
{
    public class Record
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string DateHicri { get; set; }
        public string DateMiladi { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string Group { get; set; }
    }
}
