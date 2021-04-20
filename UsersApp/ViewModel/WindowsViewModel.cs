using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UsersApp
{
    
    /// <summary>
    /// The view model for the custom flat window
    /// </summary>
    
    public class WindowsViewModel : BaseViewModel
    {
        #region Private Member
        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window mWindow;
        #endregion

        #region Public Properties

        public string Test { get; set; } = "Hello! It's chat.";

        //public ApplicationPage CurrentPage { get; set; } = ApplicationPage.login.Login;
        #endregion

        #region Constuctor
        /// <summary>
        /// Defoult constructor
        /// </summary>
        public WindowsViewModel(Window window)
        {
            mWindow = window;

            
        }
        #endregion

    }
}
