using System.ComponentModel;

namespace Coopership.ITDeveloper.Domain.Enums
{
    public enum TipoSaidaPaciente
    {
        [Description("Recebeu Alta")] Alta = 1,
        [Description("Transferido")] Transferencia,
        [Description("Saiu à Revelia")] ARevelia,
        [Description("Veio à Óbito")] Obito,
        Outros
    }
}
