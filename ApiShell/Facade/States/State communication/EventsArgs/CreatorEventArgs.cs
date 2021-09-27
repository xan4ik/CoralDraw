using System;
using System.Collections.Generic;
using System.Text;
using UseCases;

namespace ApiShell
{
    class CreatorEventArgs
    {
        public IFigureCreator Creator;
        public RedactorCore Core;
        public string Key;
    }
}
