using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_V2.Model
{
    class ParaSaveBackUp
    {
        public void ExecutParallelBackup()
        {
            State state = new State();
            var array = state.GetStateArray();
            Parallel.For(0, array.BackUpList.Count, (x) =>
            {
                var type = "complete";
                Task t = new Task(() => ExecuterSauvegarde(array.BackUpList[x], type));
                t.Start();

            });
        }
        public void ExecuterSauvegarde(State save, string type)
        {
            if (type == "DifferentialBackUp")
            {
                DifferentialBackUp BackUp = new DifferentialBackUp(save.Name, save.SourcePath, save.DestinationPath);
                BackUp.MakeBackup();
            }
            else
            {
                CompleteBackUp BackUp = new CompleteBackUp(save.Name, save.SourcePath, save.DestinationPath);
                BackUp.MakeBackup();
            }
        }

    }
}
