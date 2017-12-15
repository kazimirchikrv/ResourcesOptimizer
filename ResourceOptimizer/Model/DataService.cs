using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Resources;

namespace ResourceOptimizer.Model
{
    public class DataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            var item = new DataItem(Constants.WELCOM, Constants.FIRSTSTEP, Constants.SECONDSTEP, Constants.THIRDSTEP, Constants.FOURTHDSTEP, Constants.FIFTHSTEP, Constants.SUCCESS);
            callback(item, null);
        }

        public string SelectedPath { get; set; }
        public string GenericResourceFilePath { get; set; }
        public string ProjectFilePath { get; set; }
        public int CountFiles { get; set; } = 0;
        public int CountFilesForChanges { get; set; } = 0;
        public bool IsFirstStepDone { get; set; } = false;
        public bool IsSecondStepDone { get; set; } = false;
        public bool IsThirdStepDone { get; set; } = false;
        public bool IsNeedShowFiles { get; set; } = false;
        public bool IsFourthStepDone { get; set; } = false;
        public bool IsFifthStepDone { get; set; } = false;
        public bool IsFinished { get; set; } = false;
        public ResXResourceReader Rsxr { get; set; }

        public ObservableCollection<ResourceFile> Files { get; set; }
        public List<ResourceDescription> Resources { get; set; }
        public ObservableCollection<InvestigationFile> FilesForInvestigation { get; set; }
        public List<ChangedString> ChangedStrings { get; set; }
        
    }
}