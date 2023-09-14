namespace dropout_dl.Common.Models
{
    public class DropoutCookie
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int Length => Value.Length;

        public DropoutCookie()
        {
            Name = "";
            Value = "";
        }

        public DropoutCookie(string name)
        {
            Name = name;
            Value = "";
        }

        public DropoutCookie(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public DropoutCookie(string value, int length)
        {
            Value = value;
            Name = "?";
        }

        public DropoutCookie(string name, string value, int length)
        {
            Name = name;
            Value = value;
        }

        public void UrlDecode()
        {
            var outValue = "";
            for (var i = 0; i < Value.Length - 3; i++)
            {
                if (Value.Substring(i, 3) == "%3D")
                {
                    outValue += "=";
                    i += 2;
                }
                else
                {
                    outValue += Value[i];
                }
            }

            Value = outValue;
        }
    }
}
