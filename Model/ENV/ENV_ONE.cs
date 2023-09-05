using System.Runtime.CompilerServices;

namespace BAN_BANH.Model.env
{
    public abstract class ENV_ONE
    {
        public abstract string URL_ORDER_API { get; }
    }


    public  class ENV_DEV : ENV_ONE
    {
        public override string URL_ORDER_API { get => ENV_KEY.ENV_API_ORDER_DEV; }
    }

    public class ENV_PRODUCT : ENV_ONE
    {
        public override string URL_ORDER_API { get => ENV_KEY.ENV_API_ORDER_PRO;  }
    }


    public static class ENV_VARIBLE
    {
        public static ENV_ONE  GET_ENV_VARIBLE()
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
