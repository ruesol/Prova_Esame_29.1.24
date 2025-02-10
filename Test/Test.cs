using Prova_Esame_29._1._24;

namespace Test
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SanityCheck()
        {
            Func<int, int> ff = x => x;
            var source = new[] { ff };
            var result = source.IntersectOn(source, 0);
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Test_IntersectOn_SameFunctions()
        {
            Func<int, int> f1 = x => x + 1;
            Func<int, int> f2 = x => x + 1;
            var source = new[] { f1 };
            var other = new[] { f2 };
            var result = source.IntersectOn(other, 1);
            Assert.That(result, Is.EqualTo(new bool?[] { true }));
        }

        [Test]
        public void Test_IntersectOn_DifferentFunctions()
        {
            Func<int, int> f1 = x => x + 1;
            Func<int, int> f2 = x => x + 2;
            var source = new[] { f1 };
            var other = new[] { f2 };
            var result = source.IntersectOn(other, 1);
            Assert.That(result, Is.EqualTo(new bool?[] { false }));
        }

        [Test]
        public void Test_IntersectOn_ExceptionHandling()
        {
            Func<int, int> f1 = x => x + 1;
            Func<int, int> f2 = x => throw new Exception("Test exception");
            var source = new[] { f1 };
            var other = new[] { f2 };
            var result = source.IntersectOn(other, 1);
            Assert.That(result, Is.EqualTo(new bool?[] { null }));
        }

        [Test]
        public void Test_IntersectOn_DifferentLengths()
        {
            Func<int, int> f1 = x => x + 1;
            var source = new[] { f1 };
            var other = new[] { f1, f1 };
            //Assert.Throws<ArgumentException>(() => source.IntersectOn(other, 1));
            Assert.That(() => source.IntersectOn(other, 1), Throws.TypeOf<ArgumentException>());  
        }

        // test per l'es 2 dell'esame

        /*
         * 1. Input della chiamata sotto test:
        source è la seguente sequenza di funzioni da interi a booleani
        • la funzione che restituisce sempre vero
        • la funzione che restituisce vero sugli argomenti multipli di 2
        • la funzione che restituisce sempre falso
        other è la seguente sequenza di funzioni da interi a booleani
        • la funzione che restituisce vero sugli argomenti multipli di 3
        • la funzione che restituisce vero sugli argomenti minori di 10
        • la funzione che restituisce vero sugli argomenti multipli di 5
        p è 6.
        Output atteso: la sequenza true , true , true
        */
        [Test]
        public void Test1()
        {
            Func<int, bool> source1 = x => true;
            Func<int, bool> source2 = x => x % 2 == 0;
            Func<int, bool> source3 = x => false;
            var source = new[] { source1, source2, source3 };

            Func<int, bool> other1 = x => x % 3 == 0;
            Func<int, bool> other2 = x => x < 10;
            Func<int, bool> other3 = x => x % 5 == 0;
            var other = new[] { other1, other2, other3 };

            Assert.That(source.IntersectOn(other, 6), Is.EqualTo(new bool?[] { true, true, true }));
        }

        
        [Test]
        public void Test3()
        {
            IEnumerable<Func<string, char>> Other()
            {
                while (true)
                    yield return s => 'a';
            }
            var source = new Func<string, char>[] { s => 'b' };
            Assert.That(() => source.IntersectOn(Other(), "pippo").ToArray(),
            Throws.TypeOf<ArgumentException>());
        }
        /*
         * 4. Input della chiamata sotto test:
            source è la seguente sequenza di funzioni da interi a booleani
            • la funzione che restituisce sempre vero
            • la funzione che solleva ArgumentException sugli argomenti multipli di 2 e restituisce vero
            sugli altri valori
            • la funzione che restituisce vero sugli argomenti minori di 7
            other è la seguente sequenza di funzioni da interi a booleani
            • la funzione che restituisce sempre falso
            • la funzione che restituisce vero sugli argomenti minori di 10
            • la funzione che restituisce vero sugli argomenti multipli di 5
            p è 666.
            Output atteso: la sequenza false , null , true
        */
        [Test]
        public void Test4()
        {
            var source = new Func<int, bool>[] { x => true, x => x % 2 == 0 ? throw new ArgumentException() : true, x => x < 7 };
            var other = new Func<int, bool>[] { x => false, x => x < 10, x => x % 8 == 0 };
            Assert.That(source.IntersectOn(other, 666), Is.EqualTo(new bool?[] { false, null, true }));
        }
    }
}