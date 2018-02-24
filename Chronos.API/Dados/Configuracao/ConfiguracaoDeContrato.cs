using Chronos.API.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chronos.API.Dados.Configuracao
{
    public class ConfiguracaoDeContrato : IEntityTypeConfiguration<Contrato>
    {
        public void Configure(EntityTypeBuilder<Contrato> builder)
        {
            builder.Property(contrato => contrato.ValorDaHora)
                .HasColumnType("money");
        }
    }
}