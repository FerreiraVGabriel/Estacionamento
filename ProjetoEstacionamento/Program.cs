using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento
{
    class Program
    {
        static void Main(string[] args)
        {
            //Cadastro número de vagas
            VagasEstacionamento vagasEstacionamento = new VagasEstacionamento();

            //Entrada de vagas pequenas
            vagasEstacionamento.numVagasPequena = EntradaVagasPequenas();
            vagasEstacionamento.numVagasPequenaAtual = vagasEstacionamento.numVagasPequena;

            //Entrada de vagas Grandes                
            vagasEstacionamento.numVagasGrande = EntradaVagasGrandes();
            vagasEstacionamento.numVagasGrandeAtual = vagasEstacionamento.numVagasGrande;

            //Total de vagas
            vagasEstacionamento.numVagasTotal = vagasEstacionamento.numVagasGrandeAtual + vagasEstacionamento.numVagasPequenaAtual;

            //Informações das vagas na tela
            Console.WriteLine("Número total de vagas: {0}", vagasEstacionamento.numVagasTotal);
            Console.WriteLine("Número total de vagas pequenas: {0}", vagasEstacionamento.numVagasPequenaAtual);
            Console.WriteLine("Número total de vagas grandes: {0}", vagasEstacionamento.numVagasGrandeAtual);


            //informar tipo de veiculo entrando no estacionamento
            string saindoEntrando = "E";
            string tipoVeiculo = TipoVeiculo(saindoEntrando);

            //Verificar se o estacionamento possui vagas
            if (!PossuiVagas(vagasEstacionamento, tipoVeiculo))
            {
                Console.WriteLine("Estacionamento sem vagas.");
                return;
            }

            //Contador de Vagas- Veiculo entrou no estacionamneto
            vagasEstacionamento = ContadorVagasVeiculoEntrou(vagasEstacionamento, tipoVeiculo);

            //Contador de veiculos - Veiculo entrou no estacionamneto
            vagasEstacionamento = AdicionarVeiculos(vagasEstacionamento, tipoVeiculo);

            //Informações das vagas e contador na tela
            InformacoesVagas(vagasEstacionamento);

            while (true)
            {
                saindoEntrando = EntradaOUSaida(vagasEstacionamento);
                tipoVeiculo = TipoVeiculo(saindoEntrando);

                if (saindoEntrando == "E")
                {
                    //Verificar se o estacionamento possui vagas
                    if (!PossuiVagas(vagasEstacionamento, tipoVeiculo))
                        Console.WriteLine("Estacionamento sem vagas.");
                    else
                    {
                        //Contador de Vagas- Veiculo entrou no estacionamneto
                        vagasEstacionamento = ContadorVagasVeiculoEntrou(vagasEstacionamento, tipoVeiculo);

                        //Contador de veiculos - Veiculo entrou no estacionamneto
                        vagasEstacionamento = AdicionarVeiculos(vagasEstacionamento, tipoVeiculo);

                        //Informações das vagas e contador na tela
                        InformacoesVagas(vagasEstacionamento);
                    }

                }
                else
                {
                    //Verificar se o estacionamento possui veiculos
                    bool possuiVeiculos = PossuiVeiculos(vagasEstacionamento, tipoVeiculo);
                    while (!possuiVeiculos)
                    {
                        Console.WriteLine("Estacionamento sem veiculos do tipo {0}.", tipoVeiculo);
                        tipoVeiculo = TipoVeiculo(saindoEntrando);
                        possuiVeiculos = PossuiVeiculos(vagasEstacionamento, tipoVeiculo);
                    }


                    //Contador de Vagas- Veiculo saiu do estacionamneto
                    vagasEstacionamento = ContadorVagasVeiculoSaiu(vagasEstacionamento, tipoVeiculo);

                    //Contador de veiculos - Veiculo saiu do estacionamneto
                    vagasEstacionamento = RetirarVeiculos(vagasEstacionamento, tipoVeiculo);

                    //Informações das vagas e contador na tela
                    InformacoesVagas(vagasEstacionamento);
                }

            }
        }

        //Retorna para o usuario digitar o numero de vagas pequenas no estacionamento
        private static int EntradaVagasPequenas()
        {
            Console.WriteLine("Digite o número de vagas pequenas:");
            int numVagasPequenaAtual;
            var numVagasPequena = Console.ReadLine();

            while ((!int.TryParse(numVagasPequena, out numVagasPequenaAtual)) || numVagasPequenaAtual <= 0)
            {
                Console.WriteLine("Por favor, digite valores do tipo inteiro e maiores que zero.");
                numVagasPequena = Console.ReadLine();
            }

            return numVagasPequenaAtual;
        }

        //Retorna para o usuario digitar o numero de vagas grandes no estacionamento
        private static int EntradaVagasGrandes()
        {
            Console.WriteLine("Digite o número de vagas grandes:");
            int numVagasGrandeAtual;
            var numVagasGrandes = Console.ReadLine();

            while ((!int.TryParse(numVagasGrandes, out numVagasGrandeAtual)) || numVagasGrandeAtual <= 0)
            {
                Console.WriteLine("Por favor, digite valores do tipo inteiro e maiores que zero.");
                numVagasGrandes = Console.ReadLine();
            }

            return numVagasGrandeAtual;
        }

        //Retorna o tipo de veiculo se é 'C'=carro 'V'=van  'M'=moto
        private static string TipoVeiculo(string entradaSaida)
        {
            if (entradaSaida == "E")
                Console.WriteLine("Digite o tipo de veículo que entrará no estacionamento ('C'=carro; 'V'=van;  'M'=moto):");
            else
                Console.WriteLine("Digite o tipo de veículo que sairá do estacionamento ('C'=carro; 'V'=van;  'M'=moto):");
            //informar tipo de veiculo entrando no estacionamento
            string tipoVeiculo = Console.ReadLine();
            while (!RetornoTipoVeiculoValido(tipoVeiculo))
            {
                Console.WriteLine("Por favor, informe um veículo do tipo válido.");
                tipoVeiculo = Console.ReadLine();
            }

            return tipoVeiculo.ToUpper();
        }

        //Retorna se o veiculo esta entrando ou saindo do estacionamento
        private static string EntradaOUSaida(VagasEstacionamento vagasEstacionamento)
        {
            if (vagasEstacionamento.contCarro == 0 && vagasEstacionamento.contMoto == 0 && vagasEstacionamento.contVan == 0)
            {
                Console.WriteLine("O veículo esta entrando no estacionamento");
                return "E";
            }

            if (vagasEstacionamento.numVagasGrandeAtual == 0 && vagasEstacionamento.numVagasPequenaAtual == 0)
            {
                Console.WriteLine("O veículo esta saindo no estacionamento");
                return "S";
            }

            Console.WriteLine("Digite se o veiculo esta saindo ou entrando no estacionamento ('E'=Entrando 'S'=Saindo): ");
            string saindoEntrando = Console.ReadLine();
            while (!RetornoSaindoOUEntrando(saindoEntrando))
            {
                Console.WriteLine("Por favor, informe uma entrada do tipo válido");
                saindoEntrando = Console.ReadLine();
            }

            return saindoEntrando.ToUpper();
        }

        //Retorna as informações no estacionamento
        private static void InformacoesVagas(VagasEstacionamento vagasEstacionamento)
        {
            Console.WriteLine("Número total de vagas: {0}", vagasEstacionamento.numVagasTotal);
            Console.WriteLine("Número total de vagas pequenas: {0}", vagasEstacionamento.numVagasPequenaAtual);
            Console.WriteLine("Número total de vagas grandes: {0}", vagasEstacionamento.numVagasGrandeAtual);
            Console.WriteLine("Número total de carros: {0}", vagasEstacionamento.contCarro);
            Console.WriteLine("Número total de motos: {0}", vagasEstacionamento.contMoto);
            Console.WriteLine("Número total de vans: {0}", vagasEstacionamento.contVan);
        }

        //Retorna se o tipo de veiculo digitado pelo usuario é valido
        private static bool RetornoTipoVeiculoValido(string veiculo)
        {
            switch (veiculo.ToUpper())
            {
                case "C":
                    return true;
                case "V":
                    return true;
                case "M":
                    return true;
                default:
                    return false;
            }
        }

        //Retorna se o veiculo esta saindo ou entrando no estacionamento
        private static bool RetornoSaindoOUEntrando(string saindoEntrando)
        {
            switch (saindoEntrando.ToUpper())
            {
                case "S":
                    return true;
                case "E":
                    return true;
                default:
                    return false;
            }
        }

        //Verifica se o estacionamento possui vagas
        private static bool PossuiVagas(VagasEstacionamento vagasEstacionamento, string tipoVeiculo)
        {
            if (!VagasVan(vagasEstacionamento, tipoVeiculo))
                return false;
            else if (!VagasCarro(vagasEstacionamento, tipoVeiculo))
                return false;
            else if (!VagasMoto(vagasEstacionamento, tipoVeiculo))
                return false;
            else
                return true;
        }

        //Verificar se possui o veiculo escolhido no estacionamento
        private static bool PossuiVeiculos(VagasEstacionamento vagasEstacionamento, string tipoVeiculo)
        {
            if (vagasEstacionamento.contVan == 0 && tipoVeiculo == "V")
                return false;
            else if (vagasEstacionamento.contCarro == 0 && tipoVeiculo == "C")
                return false;
            else if (vagasEstacionamento.contMoto == 0 && tipoVeiculo == "M")
                return false;
            else
                return true;
        }

        //Verifica a disponibilidade de vagas para Van
        private static bool VagasVan(VagasEstacionamento vagasEstacionamento, string tipoVeiculo)
        {
            if ((tipoVeiculo == "V") && (vagasEstacionamento.numVagasGrandeAtual == 0 && vagasEstacionamento.numVagasPequenaAtual < 3))
                return false;
            else
                return true;
        }

        //Verifica a disponibilidade de vagas para Carro
        private static bool VagasCarro(VagasEstacionamento vagasEstacionamento, string tipoVeiculo)
        {
            if ((tipoVeiculo == "C") && (vagasEstacionamento.numVagasGrandeAtual == 0 && vagasEstacionamento.numVagasPequenaAtual == 0))
                return false;
            else
                return true;
        }

        //Verifica a disponibilidade de vagas para Moto
        private static bool VagasMoto(VagasEstacionamento vagasEstacionamento, string tipoVeiculo)
        {
            if ((tipoVeiculo == "M") && (vagasEstacionamento.numVagasGrandeAtual == 0 && vagasEstacionamento.numVagasPequenaAtual == 0))
                return false;
            else
                return true;
        }

        //Contador de Vagas - Veiculo Entrou
        private static VagasEstacionamento ContadorVagasVeiculoEntrou(VagasEstacionamento vagasEstacionamento, string tipoVeiculo)
        {
            if (tipoVeiculo == "V")
            {
                if (vagasEstacionamento.numVagasGrandeAtual == 0)
                    vagasEstacionamento.numVagasPequenaAtual -= 3;
                else
                    vagasEstacionamento.numVagasGrandeAtual -= 1;
            }
            else
            {
                if (vagasEstacionamento.numVagasPequenaAtual == 0)
                    vagasEstacionamento.numVagasGrandeAtual -= 1;
                else
                    vagasEstacionamento.numVagasPequenaAtual -= 1;
            }
            vagasEstacionamento.numVagasTotal = vagasEstacionamento.numVagasPequenaAtual + vagasEstacionamento.numVagasGrandeAtual;

            return vagasEstacionamento;
        }

        //Contador de Vagas - Veiculo Saiu
        private static VagasEstacionamento ContadorVagasVeiculoSaiu(VagasEstacionamento vagasEstacionamento, string tipoVeiculo)
        {
            if (tipoVeiculo == "V")
            {
                if (vagasEstacionamento.numVagasGrande == vagasEstacionamento.numVagasGrandeAtual)
                    vagasEstacionamento.numVagasPequenaAtual += 3;
                else
                    vagasEstacionamento.numVagasGrandeAtual += 1;
            }
            else
            {
                if (vagasEstacionamento.numVagasPequenaAtual == vagasEstacionamento.numVagasPequena)
                    vagasEstacionamento.numVagasGrandeAtual += 1;
                else
                    vagasEstacionamento.numVagasPequenaAtual += 1;
            }
            vagasEstacionamento.numVagasTotal = vagasEstacionamento.numVagasPequenaAtual + vagasEstacionamento.numVagasGrandeAtual;

            return vagasEstacionamento;
        }

        //Contador de Veiculos - Adicionar Veiculos
        private static VagasEstacionamento AdicionarVeiculos(VagasEstacionamento vagasEstacionamento, string tipoVeiculo)
        {
            if (tipoVeiculo == "C")
                vagasEstacionamento.contCarro += 1;
            else if (tipoVeiculo == "V")
                vagasEstacionamento.contVan += 1;
            else
                vagasEstacionamento.contMoto += 1;

            return vagasEstacionamento;
        }

        //Contador de Veiculos - Retirar Veiculos
        private static VagasEstacionamento RetirarVeiculos(VagasEstacionamento vagasEstacionamento, string tipoVeiculo)
        {
            if (tipoVeiculo == "C")
                vagasEstacionamento.contCarro -= 1;
            else if (tipoVeiculo == "V")
                vagasEstacionamento.contVan -= 1;
            else
                vagasEstacionamento.contMoto -= 1;

            return vagasEstacionamento;
        }




        private class VagasEstacionamento
        {
            public int numVagasTotal { get; set; }
            public int numVagasPequena { get; set; }
            public int numVagasGrande { get; set; }

            public int numVagasPequenaAtual { get; set; }
            public int numVagasGrandeAtual { get; set; }
            public int contCarro { get; set; }
            public int contMoto { get; set; }
            public int contVan { get; set; }
        }



    }
}