using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMF
{
    public class ProjectElementGeometry: UidModel
    {
        public double[] origin { get; set; }
        public virtual int LocationLength()
        {
            throw new NotImplementedException("Location Length not implemented");
        }

        public virtual int NumberOfNodes()
        {
            throw new NotImplementedException("Location Length not implemented");
        }

        public virtual int NumberOfCells()
        {
            throw new NotImplementedException("Location Length not implemented");
        }
    }
}
