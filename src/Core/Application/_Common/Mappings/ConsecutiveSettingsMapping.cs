using InventarioBackend.src.Core.Domain.Settings.Entities;
using Mapster;

namespace InventarioBackend.src.Core.Application._Common.Mappings
{
    public class ConsecutiveSettingsMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ConsecutiveSettings, ConsecutiveSettings>();
        }
    }
}
