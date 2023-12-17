using System;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class MotionElement : VisualElement
    {
        private const string SelectedClass = "selected";
        private readonly Button favourite;
        private new readonly Button name;
        private readonly MotionTypeInfo motionTypeInfo;
        internal Action<bool> OnFavouriteChanged { get; set; }
        internal Action<MotionTypeInfo> OnSelected { get; set; }

        internal MotionElement(MotionTypeInfo motionTypeInfo, bool isFavourite)
        {
            this.LoadUxml();
            this.motionTypeInfo = motionTypeInfo;
            favourite = this.Query<Button>("favourite").First();
            name = this.Query<Button>("name").First();
            Init(isFavourite);
            Bind();
        }

        private void Init(bool isFavourite)
        {
            SetIsFavourite(isFavourite);
            name.text = motionTypeInfo.Names.Preferred;
            name.tooltip = motionTypeInfo.GetTooltip();
        }

        private void Bind()
        {
            favourite.clicked += () =>
            {
                bool isSelected = !favourite.ClassListContains(SelectedClass);
                SetIsFavourite(isSelected);
                OnFavouriteChanged?.Invoke(isSelected);
            };
            name.clicked += () =>
            {
                OnSelected?.Invoke(motionTypeInfo);
            };
        }

        private void SetIsFavourite(bool isFavourite)
        {
            if (isFavourite)
            {
                favourite.AddToClassList(SelectedClass);
            }
            else
            {
                favourite.RemoveFromClassList(SelectedClass);
            }
        }
    }
}