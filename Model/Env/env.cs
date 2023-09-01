using System.Runtime.CompilerServices;

namespace BAN_BANH.Model.env
{
    public abstract class ENV
    {
        public abstract string URL_ORDER_API { get; }
    }


    public  class ENV_DEV : ENV
    {
        public override string URL_ORDER_API { get => "https://localhost:7030/product/order"; }
    }

    public class ENV_PRODUCT : ENV
    {
        public override string URL_ORDER_API { get => "https://anip/product/order";  }
    }


    public static class ENV_VARIBLE
    {
        public static ENV  GET_ENV_VARIBLE()
        {
            var currentENV = Environment.GetEnvironmentVariable(VARIBLE.CURRENT_ENV);

            if (currentENV != null && string.Compare(currentENV, VARIBLE.ENV_DEV) == 0 )
            {
                return new ENV_DEV();
            }
            else
            {
                return new ENV_PRODUCT();
            }
        }
    }

}
