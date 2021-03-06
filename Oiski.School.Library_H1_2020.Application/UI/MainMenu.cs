﻿using System;
using System.Collections.Generic;
using System.Text;
using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using Oiski.ConsoleTech.Engine.Controls;
using Oiski.School.Library_H1_2020.Application.System;

namespace Oiski.School.Library_H1_2020.Application.UI
{
    /// <summary>
    /// Defines the main entry point for an application
    /// </summary>
    public sealed class MainMenu : MasterMenu
    {
        private static MainMenu instance = null;
        public static MainMenu Instance
        {
            get
            {
                if ( instance == null )
                {
                    instance = new MainMenu("-----Oiski's Library-----", 10)
                    {
                        LoaneesButtonAction = (s) =>
                        {
                            LoaneesMenu.Instance.GetMenu.Show();
                            Instance.ResetSelection();
                            Instance.GetMenu.Show(false);
                        },

                        BookButtonAction = (s) =>
                        {
                            BooksMenu.Instance.GetMenu.Show();
                            Instance.ResetSelection();
                            Instance.GetMenu.Show(false);
                        },

                        PrintAllDataButtonAction = (s) =>
                        {
                            Library.GetLibrary.DataToFile();
                        }
                    };

                    instance.InitMenu();
                }

                return instance;
            }
        }

        /// <summary>
        /// The action that is applied when a user selects the loanee button
        /// </summary>
        public Action<SelectableControl> LoaneesButtonAction { get; private set; }
        /// <summary>
        /// The action that is applied when a user selects the books button 
        /// </summary>
        public Action<SelectableControl> BookButtonAction { get; private set; }
        /// <summary>
        /// The action that is applied when a user selects the Print All button 
        /// </summary>
        public Action<SelectableControl> PrintAllDataButtonAction { get; private set; }

        public override void InitMenu ()
        {
            base.InitMenu();
            #region Loanees Button
            ColorableOption loaneesButton = new ColorableOption("Loanees", ControlsFontColor, ControlsBorderColor, false);
            loaneesButton.Position = new Vector2(Vector2.CenterX(loaneesButton.Size.x), HeaderPosY + HeaderOffset);
            loaneesButton.SelectedIndex = Vector2.Zero;
            loaneesButton.BorderStyle(BorderArea.Horizontal, '~');
            loaneesButton.OnSelect += LoaneesButtonAction;
            GetMenu.Controls.AddControl(loaneesButton);
            #endregion

            #region Books Button
            ColorableOption booksButton = new ColorableOption("Books", ControlsFontColor, ControlsBorderColor, false);
            booksButton.Position = new Vector2(Vector2.CenterX(booksButton.Size.x), loaneesButton.Position.y + 3);
            booksButton.SelectedIndex = new Vector2(0, 1);
            booksButton.OnSelect += BookButtonAction;
            GetMenu.Controls.AddControl(booksButton);
            #endregion

            #region Print All Button
            ColorableOption printAll = new ColorableOption("Print All Data", ControlsFontColor, ControlsBorderColor, false);
            printAll.Position = new Vector2(Vector2.CenterX(printAll.Size.x), booksButton.Position.y + 3);
            printAll.SelectedIndex = new Vector2(0, 2);
            printAll.OnSelect += PrintAllDataButtonAction;
            GetMenu.Controls.AddControl(printAll);
            #endregion 

            #region Nav Button Setup
            NavButtonAction = (s) =>
            {
                Environment.Exit(0);
            };

            SetupNavButton();
            #endregion
        }

        /// <summary>
        /// Create a new instance of type <see cref="MainMenu"/> where the <paramref name="_headerText"/> and <paramref name="_headerPosY"/> is set
        /// </summary>
        /// <param name="_headerText"></param>
        /// <param name="_headerPosY"></param>
        private MainMenu (string _headerText, int _headerPosY) : base(_headerText, _headerPosY, "Close")
        {

        }
    }
}
