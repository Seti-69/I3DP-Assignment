using System;
using Mogre;
using System.Collections.Generic;

namespace RaceGame
{
    /// <summary>
    /// This class contains methods and parameters necessary 
    /// to store guns collected by the player.
    /// </summary>
    class Armoury
    {
        List<Gun> collectedGuns;    // Collection containing player guns.
        public List<Gun> CollectedGuns
        {
            get { return collectedGuns; }
             
        }

        Gun activeGun;
        public Gun ActiveGun             // Current player gun.
        {
            get { return activeGun; }
            set { activeGun = value; }
        }
        
         bool gunChanged;
        public bool GunChanged
        {
            get { return gunChanged; }
            set { gunChanged = value; }
        }
        
        
/// <summary>
/// The constructor for the Armoury class 
/// </summary>
    public Armoury()
        {
            collectedGuns = new List<Gun>();
            activeGun = new Gun();
        }

    /// <summary>
    /// Swaps the current gun.
    /// </summary>
    /// <param name="index"></param>
    public void SwapGun(int index)
        {
            if(collectedGuns!=null && activeGun!=null)
            {
               int x= collectedGuns.Count;              // Needs modulo test % ???
               // Console.Write(x);
                if (x>0) {
                    ChangeGun(collectedGuns[index]);
                }
            }
        }
    /// <summary>
    /// Sets the current gun in use.
    /// </summary>
    /// <param name="gun"></param>
    public void ChangeGun(Gun gun)
        {
            activeGun = gun;
            gunChanged = true;
        }
        /// <summary>
        /// This method adds a new gun to the armoury collection 
        /// </summary>
        /// <param name="gun"></param>
        public void AddGun(Gun gun)
        {
            bool add = new bool();
            add = true;
            foreach(Gun g in collectedGuns)
            {
                if(add && g.GetType()==gun.GetType())
                {
                    g.ReloadAmmo();
                    ChangeGun(g);
                    add = false;
                }
            }
            if(add)
            {
                ChangeGun(gun);
                collectedGuns.Add(gun);

            }else
            {
                gun.Dispose();
            }
            
        }

        /// <summary>
        /// Disposes of all gun objects in armoury
        /// </summary>
        public void Dispose()
        {
            foreach (Gun gun in collectedGuns)
            {
                gun.Dispose();
            }
            if (activeGun != null)
            {
                activeGun.Dispose();
            }
        }
    }
}
