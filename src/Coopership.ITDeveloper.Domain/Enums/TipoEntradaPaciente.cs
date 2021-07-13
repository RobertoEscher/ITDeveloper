using System.ComponentModel;

namespace Coopership.ITDeveloper.Domain.Enums
{
    public enum TipoEntradaPaciente
    {
        [Description("Internação")] Internacao = 1,
        [Description("Emergência")] Emergencia,
        [Description("Transferência")] Transferencia,
        [Description("Ambulatório")] Ambulatorio,
        [Description("Sem Prontuário")] SemProntuario
    }
}
