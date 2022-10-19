using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace AzurLaneDatabase
{
    //AzurLaneDatabase.Resources.
    public partial class MainWindow : Window {

        public static MainWindow Instance { get; private set; }

        [DllImport("Kernel32")]
        public static extern void AllocConsole();
        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        UIManager uiManager;
        LoadDB dbManager;

        string title = "";
        string titleUnsaved = "";

        public MainWindow() {
            Instance = this;
            InitializeComponent();

            title = Title;
            titleUnsaved = Title + "*";

            dbManager = new LoadDB();
            uiManager = new UIManager();

            for(int i = 0; i < dbManager.characters.Count; i++) {
                uiManager.CreateNewCharacterWindow(dbManager.characters[i]);
            }
            PreviewKeyDown += KeyboardKeysDown;
        }

        bool unsavedChanges = false;

        public void ChangeTitle(bool unsaved) {
            unsavedChanges = unsaved;
            if(unsaved) {
                Title = titleUnsaved;
            } else {
                Title = title;
            }
        }

        void KeyboardKeysDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control) {
                dbManager.Save();
            }
        }

        bool consoleOpen = false;

        public void CloseConsole() {
            if(!consoleOpen) return;
            consoleOpen = false;
            FreeConsole();
        }

        public void OpenConsole() {
            if(consoleOpen) return;
            consoleOpen = true;
            AllocConsole();
        }

        void MainWindow_Closing(object sender, CancelEventArgs e) {
            if(unsavedChanges) {
                MessageBoxResult result = MessageBox.Show("You have unsaved progress, quit without saving?", "Unsaved Progress", MessageBoxButton.YesNoCancel);
                switch(result) {
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                    case MessageBoxResult.Yes:
                        break;
                    case MessageBoxResult.No:
                        dbManager.Save();
                        break;
                    default:
                        break;
                }
            }
        }

        
    }
}
