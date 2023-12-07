using System.Transactions;

namespace LightCV.BL.General;

public static class Helpers
{
    public static int? StringToIntDef(string str, int? def)
    {
        int value;
        if (int.TryParse(str, out value))
        {
            return value;
        }

        return def;
    }
    
    public static TransactionScope CreatTransactionScope(int seconds = 60)
    {
        return new TransactionScope(TransactionScopeOption.Required,
            new TimeSpan(0, 0, seconds),
            TransactionScopeAsyncFlowOption.Enabled);
    }
}