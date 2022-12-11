namespace apiSearch.Models
{
    public class Pesquisa
    {
        public string Titulo { get; set; }
        public string Link { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var newObj = (Pesquisa)obj;
            if (this.Titulo == newObj.Titulo)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
