namespace Domain.Model;

public class Resultado<T>
{
    public ICollection<string>? Erros { get; set; }
    public bool Sucesso => Erros == null || Erros.Count == 0;
    public T? Valor { get; set; }

    public static explicit operator Resultado<T>(T valor) => new() { Valor = valor };
    public static explicit operator Resultado<T>(string erro) => new() { Erros = [erro] };

    public static Resultado<T> ComErro(string erro)
    {
        var erros = new List<string> { erro };

        return new Resultado<T> { Erros = erros };
    }

    public static Resultado<T> RetornaSucesso(T value)
    {
        return new Resultado<T> { Valor = value };
    }

    public static Resultado<T> ComErros(ICollection<string> erros)
    {
        return new Resultado<T> { Erros = erros };
    }
}