using System.Threading.Tasks;

namespace Daisy
{
    internal interface IImporter<T, I, Ruleset> where T : Storable<I, Ruleset>
    {
        T FetchNext();
        bool HasWork();
    }
}