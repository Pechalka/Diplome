namespace Diplom.Models
{
    public class Company
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string WorkedTime { get; set; }

        public string Address { get; set; }

        private string _LogoImg;

        public string LogoImg {
            get { return _LogoImg ?? "nologo.jpg"; }
            set { _LogoImg = value; }
        }

        public string Name { get; set; }

        public static Company Create(string name, string description)
        {
            return new Company
                       {
                           Name = name, 
                           Description = description
                       };
        }
    }


}