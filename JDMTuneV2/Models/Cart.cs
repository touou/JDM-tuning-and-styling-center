using System.Collections.Generic;

namespace JDMTuneV2.Models
{
    public class Cart
    {
        public static Сlothes CartInput { get; set; }
        public static bool isCartNull = true;

        public static Сlothes GetCartInput()
        {
            return CartInput;
        }
    }
}