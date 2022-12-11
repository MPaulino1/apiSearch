namespace apiSearch.Models
{
    public class FindNth
    {
        public static int findNth(String str, char ch, int N)
        {
            int occur = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ch)
                {
                    occur += 1;
                }
                if (occur == N)
                    return i;
            }
            return -1;
        }
    }
}
