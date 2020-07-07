using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Terminal.Gui;

namespace NayttoProjekti.modules
{
    class TerminalGUI
    {
        private View _curView;
        FrameView _contentPane;
        Window _leftPane;
        Dictionary<string, string> _viewClasses;
        Toplevel top;


        public void RunGUI()
        {
            Application.Init();
            top = Application.Top;

            //Copy
            List<Moduuli> testu = Enum.GetValues(typeof(Moduuli)).Cast<Moduuli>().ToList();
            _viewClasses = Enum.GetValues(typeof(Moduuli)).Cast<Moduuli>().ToList()
                .Where(t => t!=Moduuli.Lopeta)
                .Where(t => t!=Moduuli.Paanakyma)
                .OrderBy(t => t)
                .Select(t => new KeyValuePair<string, string>(t.ToString(), t.ToString().Remove(t.ToString().Length - "nakyma".Length)
                ))
                .ToDictionary(t => t.Key, t => t.Value);

            //EOF Copy

            var _menu = new MenuBar(new MenuBarItem[] {
            new MenuBarItem ("_File", new MenuItem [] {
                new MenuItem ("_Quit", "", () => {
                    Application.RequestStop ();
                })
            }),
        });

            var _leftPane = new Window("Moduulit")
            {
                X = 0,
                Y = 1,
                Width = 15,
                Height = Dim.Fill(),
                ColorScheme = Colors.TopLevel


            };
            var _moduleListView = new ListView(_viewClasses.Keys.ToList())
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(0),
                Height = Dim.Fill(0),
                AllowsMarking = false,
                ColorScheme = Colors.TopLevel,
            };
            _moduleListView.OpenSelectedItem += (a) => {
                top.SetFocus(_menu);
            };
            _moduleListView.SelectedItemChanged += (args) => {
                ClearClass(_curView);
                _curView = changeView(_viewClasses.Values.ToArray()[_moduleListView.SelectedItem]);
            };/*new ListView(Enum.GetValues(typeof(Moduuli)))
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                AllowsMarking = false,
                ColorScheme = Colors.TopLevel
            };
                */
            
            _contentPane = new FrameView("Varastonhallinta")
            {
                X = Pos.Right(_leftPane),
                Y = Pos.Bottom(_menu),
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                ColorScheme = Colors.TopLevel
            };
            _leftPane.Add(_moduleListView);
            top.Add(_menu, _contentPane, _leftPane);


            Application.Run();
        }

        //Copy
        void ClearClass(View view)
        {
            // Remove existing class, if any
            if (view != null)
            {
                _contentPane.Remove(view);
                _contentPane.Clear();
            }
        }


        //EOF COPY
        private View changeView(string moduuli)
        {
            switch (moduuli.ToLower())
            {
                case "myynti":
                    {
                        myyntiFrame();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            /*
            var view = (View)Activator.CreateInstance(type);

            view.ColorScheme = Colors.Base;
            _contentPane.Add(view);
            return view;
            */
            _contentPane.Title = moduuli;
            return _curView;
        }

        private void myyntiFrame()
        {
            var view = new FrameView
            throw new NotImplementedException();
        }
    }
}
