public class FolhaPagamento
{
    public FolhaPagamento(
        string nome,
        string cpf,
        int mes,
        int ano,
        double horasTrabalhadas,
        double valorHora
    ) {
        Id = Guid.NewGuid().ToString();
        Nome = nome;
        Cpf = cpf;
        Mes = mes;
        Ano = ano;
        HorasTrabalhadas = horasTrabalhadas;
        ValorHora = valorHora;

        SalarioBruto = horasTrabalhadas * valorHora;
        ImpostoRenda = CalcIr(SalarioBruto);
        Inss = CalcInss(SalarioBruto);
        Fgts = SalarioBruto * .08;
        SalarioLiquido = SalarioBruto - Inss - ImpostoRenda;
    }

    static private double CalcIr(double salarioBruto) {
        if (salarioBruto < 1903.99) {
            return 0;
        }

        if (salarioBruto < 2826.66) {
            return salarioBruto * .075 - 142.80;
        }

        if (salarioBruto < 3751.06) {
            return salarioBruto * .15 - 354.80;
        }

        if (salarioBruto < 4664.69) {
            return salarioBruto * .225 - 636.13;
        }

        return salarioBruto * .275 - 869.36;
    }

    static private double CalcInss(double salarioBruto) {
        if (salarioBruto < 1693.73) {
            return salarioBruto * .08;
        }

        if (salarioBruto < 2822.91) {
            return salarioBruto * .09;
        }

        if (salarioBruto < 5645.81) {
            return salarioBruto * .11;
        }

        return 621.03;
    }

    public string Id {get; private set;}
    public string Nome {get; private set;}
    public string Cpf {get; private set;}
    public int Mes {get; private set;}
    public int Ano {get; private set;}
    public double HorasTrabalhadas {get; private set;}
    public double ValorHora {get; private set;}
    public double SalarioBruto {get; private set;}
    public double ImpostoRenda {get; private set;}
    public double Inss {get; private set;}
    public double Fgts {get; private set;}
    public double SalarioLiquido {get; private set;}
}