namespace Prova_Esame_29._1._24
{
    public static class Es_1_Extensions
    {
        public static IEnumerable<bool?> IntersectOn<T1, T2>(this IEnumerable<Func<T1, T2>> source, IEnumerable<Func<T1, T2>> other, T1 p)
        {
            var source_enum = source.GetEnumerator();
            var other_enum = other.GetEnumerator();

            var source_next = source_enum.MoveNext();
            var other_next = other_enum.MoveNext();
            while (source_next && other_next)
            {
                bool? result;

                try
                {
                    result = source_enum.Current(p)?.Equals(other_enum.Current(p));
                }
                catch (Exception)
                {
                    result = null;
                }
                yield return result;

                source_next = source_enum.MoveNext();
                other_next = other_enum.MoveNext();
            }
            if (source_next || other_next)
            {
                throw new ArgumentException("Le due sequenze non hanno lo stesso numero di elementi.\n");
            }
        }
    }
}