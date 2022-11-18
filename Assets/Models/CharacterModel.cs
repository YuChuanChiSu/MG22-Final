using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CharacterModel
{
    public enum  Temperature
    {
        Zero = 0, Standard = 25, Boil = 100
    }
    public enum CharacterForm
    {
        Ice, Water, Mist
    }
    public enum CharacterState
    {
        Still, Walk, Jump
    }
}

