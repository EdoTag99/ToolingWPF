﻿namespace ToolingWPF.Model
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Source { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}