using CursoTDD.DominioTest._Builders;
using CursoTDD.DominioTest._Util;
using ExpectedObjects;
using Xunit.Abstractions;

namespace CursoTDD.DominioTest.Cursos
{
    public class CursoTest : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Construtor sendo executado.");

            _nome = "Informática Básica";
            _cargaHoraria = 80;
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = 950;
            _descricao = "Uma descrição";
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado.");
        }

        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao
            };

            var curso = new Curso
                (cursoEsperado.Nome,
                 cursoEsperado.Descricao,
                 cursoEsperado.CargaHoraria,
                 cursoEsperado.PublicoAlvo,
                 cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerNomeInvalido(string nomeInvalido)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
                CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem("Nome inválido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
                CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                .ComMensagem("Carga horária inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaValorMenorQue1(double valorInvalido)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
                CursoBuilder.Novo().ComValor(valorInvalido).Build())
                .ComMensagem("Valor inválido");
        }
    }

    public enum PublicoAlvo
    {
        Estudante = 1,
        Universitario = 2,
        Empregado = 3,
        Empreendedor = 4
    }

    public class Curso
    {
        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome inválido");

            if (cargaHoraria < 1)
                throw new ArgumentException("Carga horária inválida");

            if (valor < 1)
                throw new ArgumentException("Valor inválido");

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
            Descricao = descricao;
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
    }
}
