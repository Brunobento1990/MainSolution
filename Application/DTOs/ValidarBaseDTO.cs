using Application.Attributes;
using System.Reflection;

namespace Application.DTOs;

public abstract class ValidarBaseDTO
{
    public virtual ICollection<string> Validar()
    {
        ICollection<string> erros = [];

        foreach (var propriedade in GetType().GetProperties())
        {
            var valor = propriedade.GetValue(this);

            foreach (var atributo in propriedade.GetCustomAttributes<ValidarBaseAttribute>())
            {
                var erro = atributo.Validar(valor);

                if (!string.IsNullOrWhiteSpace(erro))
                {
                    erros.Add(erro);
                }
            }
        }

        return erros;
    }
}
