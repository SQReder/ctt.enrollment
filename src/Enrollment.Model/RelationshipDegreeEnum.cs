using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Enrollment.Model
{
    public enum RelationTypeEnum
    {
        Child = 0, // Сын/дочь
        Grandchild = 1, // Внук/внучка
        Ward = 2, // Опекаемый
    }
}
