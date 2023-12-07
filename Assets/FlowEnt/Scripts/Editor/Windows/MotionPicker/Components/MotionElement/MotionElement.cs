using System;
using System.Collections;
using System.Collections.Generic;
using PlasticGui.WorkspaceWindow;
using UnityEngine;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    public class MotionElement : VisualElement
    {
        private string SelectedClass = "selected";
        private Button favourite;

        public Action<bool> OnFavouriteChanged { get; set; }

        public MotionElement()
        {
            this.LoadUxml();
            favourite = this.Query<Button>("favourite").First();
            Bind();
        }

        private void Bind()
        {
            favourite.clicked += () =>
            {
                bool isSelected;
                if (favourite.ClassListContains(SelectedClass))
                {
                    favourite.RemoveFromClassList(SelectedClass);
                    isSelected = false;
                }
                else
                {
                    favourite.AddToClassList(SelectedClass);
                    isSelected = true;
                }

                OnFavouriteChanged?.Invoke(isSelected);
            };
        }
    }
}