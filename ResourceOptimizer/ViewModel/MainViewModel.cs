using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ResourceOptimizer.Model;
using ResourceOptimizer.Domain;
using MessageBox = System.Windows.MessageBox;

namespace ResourceOptimizer.ViewModel
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IDataService _dataService;

        #region Constants
        public const string WelcomeTitlePropertyName = "WelcomeTitle";
        public const string FirstStepPropertyName = "FirstStep";
        public const string SecondStepPropertyName = "SecondStep";
        public const string ThirdPropertyName = "ThirdStep";
        public const string FourthPropertyName = "FourthStep";
        public const string FifthPropertyName = "FifthStep";

        private string _welcomeTitle = string.Empty;
        private string _firstStep = string.Empty;
        private string _secondStep = string.Empty;
        private string _thirdStep = string.Empty;
        private string _fourthStep = string.Empty;
        private string _fifthStep = string.Empty;
        private string _succesMessage = string.Empty;

        private ICommand _selectPahCmd;
        private ICommand _selectPahToGenericFileCmd;
        private ICommand _selectPathToPathToCmd;
        private ICommand _getAllResourceFilesCmd;
        private ICommand _getAllFilesForInvestigatingCmd;
        private ICommand _seeAllResourceFilesCmd;
        private ICommand _collectAllVariablesCmd;
        private ICommand _replacePiecesOfCodeCmd;
        #endregion

        #region Properties
        public string WelcomeTitle
        {
            get => _welcomeTitle;
            set => Set(ref _welcomeTitle, value);
        }

        public string FirstStep
        {
            get => _firstStep;
            set => Set(ref _firstStep, value);
        }

        public string SecondStep
        {
            get => _secondStep;
            set => Set(ref _secondStep, value);
        }

        public string ThirdStep
        {
            get => _thirdStep;
            set => Set(ref _thirdStep, value);
        }

        public string FourthStep
        {
            get => _fourthStep;
            set => Set(ref _fourthStep, value);
        }

        public string FifthStep
        {
            get => _fifthStep;
            set => Set(ref _fifthStep, value);
        }

        public string SuccesMessage
        {
            get => _succesMessage;
            set => Set(ref _succesMessage, value);
        }


        public string SelectedPath
        {
            get => _dataService.SelectedPath;
            set
            {
                _dataService.SelectedPath = value;
                OnPropertyChanged("SelectedPath");
            }
        }

        public int CountFiles
        {
            get => _dataService.CountFiles;
            set
            {
                _dataService.CountFiles = value;
                OnPropertyChanged("CountFiles");
            }
        }

        public int CountFilesForChanges
        {
            get => _dataService.CountFilesForChanges;
            set
            {
                _dataService.CountFilesForChanges = value;
                OnPropertyChanged("CountFilesForChanges");
            }
        }

        public string GenericResourceFilePath
        {
            get => _dataService.GenericResourceFilePath;
            set
            {
                _dataService.GenericResourceFilePath = value;
                OnPropertyChanged("GenericResourceFilePath");
            }
        }

        public string ProjectFilePath
        {
            get => _dataService.ProjectFilePath;
            set
            {
                _dataService.ProjectFilePath = value;
                OnPropertyChanged("ProjectFilePath");
            }
        }

        public ObservableCollection<ResourceFile> LisResources
        {
            get => _dataService.Files;
            set
            {
                _dataService.Files = value;
                OnPropertyChanged("LisResources");
            }
        }

        public ObservableCollection<InvestigationFile> LisFilesForChanges
        {
            get => _dataService.FilesForInvestigation;
            set
            {
                _dataService.FilesForInvestigation = value;
                OnPropertyChanged("LisFilesForChanges");
            }
        }

        public bool IsFirstStepDone
        {
            get => _dataService.IsFirstStepDone;
            set
            {
                _dataService.IsFirstStepDone = value;
                OnPropertyChanged("IsFirstStepDone");
            }
        }

        public bool IsSecondStepDone
        {
            get => _dataService.IsSecondStepDone;
            set
            {
                _dataService.IsSecondStepDone = value;
                OnPropertyChanged("IsSecondStepDone");
            }
        }

        public bool IsThirdStepDone
        {
            get => _dataService.IsThirdStepDone;
            set
            {
                _dataService.IsThirdStepDone = value;
                OnPropertyChanged("IsThirdStepDone");
            }
        }

        public bool IsFourthStepDone
        {
            get => _dataService.IsFourthStepDone;
            set
            {
                _dataService.IsFourthStepDone = value;
                OnPropertyChanged("IsFourthStepDone");
            }
        }

        public bool IsFifthStepDone
        {
            get => _dataService.IsFifthStepDone;
            set
            {
                _dataService.IsFifthStepDone = value;
                OnPropertyChanged("IsFifthStepDone");
            }
        }


        public bool IsNeedShowFiles
        {
            get => _dataService.IsNeedShowFiles;
            set
            {
                _dataService.IsNeedShowFiles = value;
                OnPropertyChanged("IsNeedShowFiles");
            }
        }

        public bool IsFinished
        {
            get => _dataService.IsFinished;
            set
            {
                _dataService.IsFinished = value;
                OnPropertyChanged("IsFinished");
            }
        }
        #endregion

        #region Commands

        public object SelectPathCmd => _selectPahCmd;
        public object SelectPathToGenericFileCmd => _selectPahToGenericFileCmd;
        public object SelectPathToPathToCmd => _selectPathToPathToCmd;
        public object GetAllResourceFilesCmd => _getAllResourceFilesCmd;
        public object GetAllFilesForInvestigatingCmd => _getAllFilesForInvestigatingCmd;
        public object SeeAllResourceFilesCmd => _seeAllResourceFilesCmd;
        public object CollectAllVariablesCmd => _collectAllVariablesCmd;
        public object ReplacePiecesOfCodeCmd => _replacePiecesOfCodeCmd;

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _selectPahCmd = new RelayCommand<string>(SelectPath);
            _selectPahToGenericFileCmd = new RelayCommand<string>(SelectPath);
            _selectPathToPathToCmd = new RelayCommand<string>(SelectPath);
            _getAllResourceFilesCmd = new RelayCommand(GetAllResourceFiles);
            _getAllFilesForInvestigatingCmd = new RelayCommand(GetAllFilesForInvestigating);
            _seeAllResourceFilesCmd = new RelayCommand(SeeAllResourceFiles);
            _collectAllVariablesCmd = new RelayCommand(CollectAllVariablesToFile);
            _replacePiecesOfCodeCmd = new RelayCommand(ReplacePiecesOfCode);
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        return;
                    }

                    WelcomeTitle = item.Title;
                    FirstStep = item.FirstStep;
                    SecondStep = item.SecondStep;
                    ThirdStep = item.ThirdStep;
                    FourthStep = item.FourthStep;
                    FifthStep = item.FifthStep;
                    SuccesMessage = item.SuccesMessage;
                });
        }

        private void GetAllResourceFiles()
        {
            if (_dataService.Files == null)
                _dataService.Files = new ObservableCollection<ResourceFile>();

            if (_dataService.Files.Count > 0)
                _dataService.Files.Clear();

            if (_dataService.Resources == null)
            {
                _dataService.Resources = new List<ResourceDescription>();
            }

            if (_dataService.Resources.Count > 0)
                _dataService.Resources.Clear();

            DirSearch(SelectedPath, false);
            CountFiles = _dataService.Files.Count;
            LisResources = _dataService.Files;
            IsSecondStepDone = true;
        }


        private void GetAllFilesForInvestigating()
        {
            if (_dataService.FilesForInvestigation == null)
                _dataService.FilesForInvestigation = new ObservableCollection<InvestigationFile>();

            if (_dataService.FilesForInvestigation.Count > 0)
                _dataService.FilesForInvestigation.Clear();

            _dataService.CountFilesForChanges = _dataService.FilesForInvestigation.Count;

            DirSearch(ProjectFilePath, true);
            CountFilesForChanges = _dataService.FilesForInvestigation.Count;
            LisFilesForChanges = _dataService.FilesForInvestigation;
            IsFifthStepDone = true;
        }

        private void SeeAllResourceFiles()
        {
            IsNeedShowFiles = true;
        }

        private void CollectAllVariablesToFile()
        {
            try
            {
                if (_dataService.CountFiles > 0)
                {
                    foreach (var file in _dataService.Files)
                    {
                        var fileNameLikeClass = file.FileName.Split('.')[0];
                        if (File.Exists(file.PathToFile))
                        {
                            #region GetAllFiles
                            ResXResourceReader rsxr = new ResXResourceReader(file.PathToFile);

                            IDictionaryEnumerator id = rsxr.GetEnumerator();

                            foreach (DictionaryEntry d in rsxr)
                            {
                                string tempKey = d.Key.ToString();
                                var keyOrigin = d.Key.ToString();
                                var value = d.Value.ToString();
                                if (_dataService.Resources.FirstOrDefault(x => x.ConstatToEquls == tempKey.ToLower()) != null)
                                {
                                    //compare 2 value
                                    if (string.Equals(_dataService.Resources.FirstOrDefault(x => x.ConstatToEquls == tempKey.ToLower())?.Value, value))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        tempKey = "_" + fileNameLikeClass + "_" + tempKey;
                                        while (_dataService.Resources.FirstOrDefault(x => x.ConstatToEquls == tempKey.ToLower()) != null)
                                        {
                                            tempKey = "_" + tempKey;
                                        }
                                    }
                                }
                                var reso = new ResourceDescription() { OriginalConstantName = keyOrigin, ConstantName = tempKey, ConstatToEquls = tempKey.ToLower(), Value = value, OriginalFileName = fileNameLikeClass };
                                _dataService.Resources.Add(reso);

                            }
                            #endregion
                        }
                        else
                        {
                            MessageBox.Show("Sorry, but program can not find a file " + file.PathToFile);
                        }
                    }
                }

                ResXResourceWriter resourceWriter = new ResXResourceWriter(GenericResourceFilePath);
                foreach (dynamic res in _dataService.Resources)
                {
                    resourceWriter.AddResource(res.ConstantName, res.Value);
                }
                resourceWriter.Generate();
                resourceWriter.Close();

                _dataService.Rsxr = new ResXResourceReader(GenericResourceFilePath);

                IsFourthStepDone = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            IsFourthStepDone = true;
        }


        private void ReplacePiecesOfCode()
        {

            bool isUsedResourceNameSpace = false;

            try
            {
                if (_dataService.CountFilesForChanges > 0)
                {
                    for (int k = 0; k < _dataService.FilesForInvestigation.Count; k++)
                    {
                        isUsedResourceNameSpace = false;
                        if (File.Exists(_dataService.FilesForInvestigation[k].PathToFile))
                        {
                            #region GetAllFiles

                            List<string> wholeFileBeforeSchanges = File.ReadAllLines(_dataService.FilesForInvestigation[k].PathToFile).ToList();
                            if (wholeFileBeforeSchanges.FirstOrDefault(x => x.Contains("using Resources;")) != null)
                            {
                                isUsedResourceNameSpace = true;
                            }

                            string fileName = string.Empty;
                            string afterReplacing = string.Empty;
                            for (int i = 0; i < wholeFileBeforeSchanges.Count; i++)
                            {
                                for (int j = 0; j < _dataService.Resources.Count; j++)
                                {

                                    //[CommonRequired("EMMandatoryField", typeof(Resources.Resources))]
                                    //[LocalizedName("lblPassword", typeof(DomainResources))]
                                    //[LocalizedDescrip
                                    //var result = addresses.Select(x => _addressBookMapper.MapTo(x)); 

                                    if (wholeFileBeforeSchanges[i].Contains(_dataService.Resources[j].OriginalConstantName))
                                    {
                                        int indexOfConstantname = wholeFileBeforeSchanges[i]
                                            .IndexOf(_dataService.Resources[j].OriginalConstantName);
                                        if (indexOfConstantname == 0)
                                            continue;
                                        //TODO Process the case when there is a resource name in the line, but there is NO constant !!
                                        //TODO Если оставлять имена константы такими же, то алгоритм надо очень переделать!
                                        switch (wholeFileBeforeSchanges[i][indexOfConstantname - 1])
                                        {
                                            case '.':
                                            case '"':
                                                {
                                                    if (!char.IsLetterOrDigit(wholeFileBeforeSchanges[i][indexOfConstantname + _dataService.Resources[j].OriginalConstantName.Length]))
                                                    {
                                                        //Определили точно, что это константа, а не какой-нибудь //[DataType(DataType.Password)]
                                                        fileName = string.Empty;

                                                        if (wholeFileBeforeSchanges[i]
                                                            .Contains(_dataService.Resources[j].OriginalFileName))
                                                        {
                                                            fileName = _dataService.Resources[j].OriginalFileName;

                                                        }

                                                        afterReplacing = ReplaceCode(wholeFileBeforeSchanges[i], indexOfConstantname - 1,
                                                        _dataService.Resources[j].OriginalConstantName, fileName, isUsedResourceNameSpace);


                                                        #region Save for logs and investigation in file or DB later.

                                                        if (_dataService.ChangedStrings == null)
                                                        {
                                                            _dataService.ChangedStrings = new List<ChangedString>();
                                                        }
                                                        _dataService.ChangedStrings.Add(new ChangedString()
                                                        {
                                                            Index = i,
                                                            OldFileName = _dataService.FilesForInvestigation[k].PathToFile,
                                                            NewFileName = _dataService.FilesForInvestigation[k].PathToFile + "_OriginalFile",
                                                            StrAfter = afterReplacing,
                                                            StrBefore = wholeFileBeforeSchanges[i]
                                                        });

                                                        #endregion

                                                        // Do not good algorithm! Must refactoring later!
                                                        if (!string.IsNullOrWhiteSpace(afterReplacing))
                                                        {
                                                            if (afterReplacing.Contains("[LocalizedName(") || afterReplacing.Contains("[LocalizedDescription("))
                                                            {
                                                                if (isUsedResourceNameSpace)
                                                                {
                                                                    afterReplacing = afterReplacing.Replace("Resources.Resources.Resources", "Resources.Resources");
                                                                }
                                                                var startIndex = afterReplacing.IndexOf("Resources");
                                                                if (startIndex >= 0)
                                                                {
                                                                    string tempS = afterReplacing.Substring(startIndex);
                                                                    if (!string.IsNullOrWhiteSpace(tempS))
                                                                    {
                                                                        var stringAfterMagic = tempS.Substring(0, tempS.IndexOf(")"));
                                                                        afterReplacing = afterReplacing.Replace(stringAfterMagic, "Resources.Resources");
                                                                    }

                                                                }

                                                            }
                                                            if (afterReplacing.Contains("Resources.Resources.Resources"))
                                                            {
                                                                afterReplacing = afterReplacing.Replace("Resources.Resources.Resources", "Resources.Resources");
                                                            }
                                                        }

                                                        if (isUsedResourceNameSpace)
                                                        {
                                                            if (afterReplacing.Contains("[LocalizedName(") || afterReplacing.Contains("[LocalizedDescription("))
                                                            {
                                                                if (afterReplacing.Contains("(Resources.Resources"))
                                                                {
                                                                    afterReplacing = afterReplacing.Replace("(Resources.Resources.Resources", "(Resources.Resources");

                                                                    afterReplacing = afterReplacing.Replace("(Resources.Resources", "(Resources");
                                                                }

                                                                if (afterReplacing.Contains("(Resources)") || afterReplacing.Contains("(Resources.Resources.Resources)"))
                                                                {
                                                                    afterReplacing = afterReplacing.Replace("(Resources.Resources.Resources)", "(Resources.Resources)");
                                                                    afterReplacing = afterReplacing.Replace("(Resources)", "(Resources.Resources)");
                                                                }

                                                            }

                                                        }

                                                        //if (_dataService.FilesForInvestigation[k].PathToFile.Contains(@"Models\RiskAssessment\Jobs\BatchPrintSignatureWindowModel.")
                                                        //    ||
                                                        //    _dataService.FilesForInvestigation[k].PathToFile.Contains(@"Models\Transport\PackagingWizardModel.cs"))
                                                        //{
                                                        //    afterReplacing = afterReplacing.Replace("(Resources.Resources)", "(Resources)");
                                                        //}
                                                        wholeFileBeforeSchanges[i] = string.IsNullOrWhiteSpace(afterReplacing) ? wholeFileBeforeSchanges[i] : afterReplacing;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                            #endregion

                            File.Copy(_dataService.FilesForInvestigation[k].PathToFile, _dataService.FilesForInvestigation[k].PathToFile + "_OriginalFile");
                            File.Delete(_dataService.FilesForInvestigation[k].PathToFile);
                            File.WriteAllLines(_dataService.FilesForInvestigation[k].PathToFile, wholeFileBeforeSchanges);
                        }

                        else
                        {
                            MessageBox.Show("Sorry, but program can not find a file " + _dataService.FilesForInvestigation[k].PathToFile);
                        }
                    }
                }

                ResXResourceWriter resourceWriter = new ResXResourceWriter(GenericResourceFilePath);
                foreach (dynamic res in _dataService.Resources)
                {
                    resourceWriter.AddResource(res.ConstantName, res.Value);
                }
                resourceWriter.Generate();
                resourceWriter.Close();
                IsFourthStepDone = true;
                IsFinished = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            IsFinished = true;
        }

        private string ReplaceCode(string strForReplacing, int indexForSartreplacing, string constant, string fileName, bool isUsedResourceNameSpace)
        {
            string result = string.Empty;

            if (strForReplacing.Contains("RequiredPrivilege("))
                return result;

            try
            {
                foreach (var resource in _dataService.Resources)
                {
                    if (resource.OriginalConstantName == constant)
                    {
                        if (resource.OriginalFileName == fileName)
                        {
                            //найти эту подстроку в строке
                            var foundedIndex = strForReplacing.IndexOf(resource.OriginalFileName);

                            if (foundedIndex > 0)
                            {
                                //определить, чтобы следующий и предыдущий символ после вхождения  не был буквой или цифрой, тогда и заменять
                                if (!char.IsLetterOrDigit(strForReplacing[foundedIndex + resource.OriginalFileName.Length])
                                    && !char.IsLetterOrDigit(strForReplacing[foundedIndex - 1]))
                                {
                                    var stringRes1 = "Resources";
                                    var stringRes2 = "Resources.Resources";

                                    if (strForReplacing.Contains("[LocalizedDescription(") ||
                                        strForReplacing.Contains("[Display(ResourceType ") ||
                                        strForReplacing.Contains("[LocalizedName") ||
                                        strForReplacing.Contains("[IncludeInList(") ||
                                        strForReplacing.Contains("[IncludeInExternalLink(") ||
                                        strForReplacing.Contains("[Required") ||
                                        strForReplacing.Contains("[NoSpecialSymbols(") ||
                                        strForReplacing.Contains("[StringLength(") ||
                                        strForReplacing.Contains("ValidationResult("))
                                    {
                                        result = strForReplacing.Replace(resource.OriginalFileName, stringRes2);
                                    }
                                    else
                                    {
                                        result = strForReplacing.Replace(resource.OriginalFileName, stringRes1);
                                        if (isUsedResourceNameSpace)
                                            result = strForReplacing.Replace(resource.OriginalFileName, stringRes2);
                                    }


                                    if (result.Contains("Resources.Resources.Resources"))
                                    {
                                        result = result.Replace("Resources.Resources.Resources", "Resources.Resources");
                                    }
                                }
                                else
                                {
                                    if (result.Contains("Resources.Resources.Resources"))
                                    {
                                        result = result.Replace("Resources.Resources.Resources", "Resources.Resources");
                                    }
                                    return result;
                                }
                            }

                        }
                        string originalValue = resource.Value;

                        ResXResourceReader rsxr = _dataService.Rsxr;

                        IDictionaryEnumerator id = rsxr.GetEnumerator();

                        //Replacing Constants
                        foreach (DictionaryEntry d in rsxr)
                        {
                            var value = d.Value.ToString();
                            if (value == originalValue)
                            {
                                foreach (DictionaryEntry res in rsxr)
                                {
                                    if (res.Value.ToString() == originalValue)
                                    {
                                        if (res.Key.ToString().Contains(constant))
                                        {
                                            result = result.Replace(constant, res.Key.ToString());
                                        }
                                    }
                                }
                                return result;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            throw new Exception("Some problem with replacing!");
        }

        private void DirSearch(string dir, bool isForProject)
        {
            try
            {
                string regexMask = isForProject ? @"(?<!\.Designer)\.(cshtml|cs)$" : @"[a-zA-Z]*.resx";
                Regex reg1 = new Regex(regexMask);

                var listFiles = Directory.GetFiles(dir).Where(path => reg1.IsMatch(path)).ToList();

                listFiles.ForEach(f =>
                {
                    var list = f.Split('.');
                    StringBuilder temp = new StringBuilder(list[list.Count() - 1]).Append(list[list.Count() - 2]);
                    // The last 2 elements are contained \, which means that this is a defalted file, then it's good!
                    if (temp.ToString().Contains(@"\"))
                    {
                        var fileName = f.Split('\\').Last();
                        if (isForProject)
                        {
                            _dataService.FilesForInvestigation.Add(new InvestigationFile() { FileName = fileName, PathToFile = f });
                        }
                        else
                        {
                            _dataService.Files.Add(new ResourceFile() { FileName = fileName, PathToFile = f });
                        }
                    }
                });

                foreach (string d in Directory.GetDirectories(dir))
                {
                    DirSearch(d, isForProject);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void SelectPath(string step)
        {
            string description = string.Empty, message = string.Empty;
            Enum.TryParse(step, out PathTo myStatus);
            int numberOfStep = 0;

            switch (myStatus)
            {
                case PathTo.ResourcesFolder:
                    description = "Select folder with resources:";
                    message =
                        "The selected directory does not contain resources.\r\nMaybe some other =)\r\nAre you sure??";
                    numberOfStep = 1;
                    break;
                case PathTo.GenericFile:
                    description = "Select folder for generic resources:";
                    message =
                        "Selected folder contain file Resources.resx! It will be delete! \r\nMaybe some other directory?\r\nAre you sure??";
                    numberOfStep = 2;
                    break;
                case PathTo.RootProject:
                    description = "Select folder with project:";
                    message =
                        "Please, selected folder with project!";
                    numberOfStep = 3;
                    break;
            }

            GenerateDialog(description, message, numberOfStep);


        }

        private void GenerateDialog(string description, string message, int step)
        {
            using (var fbd = new FolderBrowserDialogEx())
            {
                fbd.Description = description;
                fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;
                fbd.ShowNewFolderButton = true;
                fbd.ShowEditBox = true;
                fbd.ShowFullPathInEditBox = true;
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !String.IsNullOrEmpty(fbd.SelectedPath))
                {
                    if (step == 1)
                    {
                        if (fbd.SelectedPath.Contains("esour"))
                        {
                            this.SelectedPath = fbd.SelectedPath;
                            this.IsFirstStepDone = true;
                        }
                        else
                        {
                            MessageBoxResult dialogResult = System.Windows.MessageBox.Show(message, "WARNING!!!", MessageBoxButton.YesNo);
                            if (dialogResult == MessageBoxResult.Yes)
                            {
                                this.SelectedPath = fbd.SelectedPath;
                                this.IsFirstStepDone = true;
                            }
                            else if (dialogResult == MessageBoxResult.No)
                            {
                                if (dialogResult == MessageBoxResult.No)
                                    return;
                            }
                        }
                    }
                    else if (step == 2)
                    {
                        GenericResourceFilePath = fbd.SelectedPath + "\\Resources.resx";
                        if (File.Exists(GenericResourceFilePath))
                        {
                            MessageBoxResult dialogResult = System.Windows.MessageBox.Show(message, "WARNING!!!", MessageBoxButton.YesNo);
                            if (dialogResult == MessageBoxResult.Yes)
                            {
                                this.GenericResourceFilePath = this.GenericResourceFilePath;
                                File.Delete(GenericResourceFilePath);
                            }
                            else if (dialogResult == MessageBoxResult.No)
                            {
                                if (dialogResult == MessageBoxResult.No)
                                    return;
                            }
                        }

                        if (File.Exists(GenericResourceFilePath))
                            File.Delete(GenericResourceFilePath);

                        IsThirdStepDone = true;
                    }
                    else if (step == 3)
                    {
                        ProjectFilePath = fbd.SelectedPath;
                        IsFourthStepDone = true;
                    }

                }
                else
                {
                    MessageBox.Show("Select correct path!", "WARNING!!!", MessageBoxButton.OK);
                }
            }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}