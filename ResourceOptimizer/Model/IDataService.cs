using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Text;

namespace ResourceOptimizer.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
        string SelectedPath { get; set; }
        string GenericResourceFilePath { get; set; }
        string ProjectFilePath { get; set; }
        int CountFiles { get; set; }
        int CountFilesForChanges { get; set; }
        bool IsFirstStepDone { get; set; }
        bool IsSecondStepDone { get; set; }
        bool IsNeedShowFiles { get; set; }
        bool IsThirdStepDone { get; set; }
        bool IsFourthStepDone { get; set; }
        bool IsFifthStepDone { get; set; }
        bool IsFinished { get; set; }
        ResXResourceReader Rsxr { get; set; }


        ObservableCollection<ResourceFile> Files { get; set; }
        List<ResourceDescription> Resources { get; set; }
        ObservableCollection<InvestigationFile> FilesForInvestigation { get; set; }

        List<ChangedString> ChangedStrings { get; set; }
    }
}
