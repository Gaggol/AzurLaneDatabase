using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace AzurLaneDatabase
{
    internal class Character
    {
        public readonly string Name;
        public BitmapImage image;
        public int maxStars;
        public int stars;
        public bool isOwned;
        public Rarity rarity;
        public bool canRetrofit;
        public bool isRetrofit;

        public Image charImage;
        public Image rarityImage;

        void CheckStarLevel() {
            switch(rarity) {
                case Rarity.Normal:
                    this.SetStars(1);
                    break;
                case Rarity.Rare:
                    this.SetStars(2);
                    break;
                case Rarity.SR:
                    this.SetStars(2);
                    break;
                case Rarity.SSR:
                    this.SetStars(3);
                    break;
                case Rarity.UR:
                    this.SetStars(3);
                    break;
            }
        }

        void SetMaxStar() {
            switch(this.rarity) {
                case Rarity.Normal:
                    maxStars = 4;
                    break;
                case Rarity.Rare:
                    maxStars = 5;
                    break;
                case Rarity.SR:
                    maxStars = 5;
                    break;
                case Rarity.SSR:
                    maxStars = 6;
                    break;
                case Rarity.UR:
                    maxStars = 6;
                    break;
            }
        }

        public Character(string Name, int rarity, int stars, bool isOwned, bool canRetrofit = false, bool isRetrofit = false) {
            this.Name = Name;
            this.stars = stars;
            this.isOwned = isOwned;
            this.canRetrofit = canRetrofit;
            this.isRetrofit = isRetrofit;

            this.rarity = (Rarity)rarity;

            SetMaxStar();

            if(stars == 0) {
                CheckStarLevel();
            }


            image = new BitmapImage();
            image.BeginInit();
            if(isRetrofit) {
                image.UriSource = new Uri(@"/AzurLaneDatabase;component/Resources/chars/" + Name.Replace(' ', '_') + "_Retrofit.png", UriKind.Relative);
            } else {
                image.UriSource = new Uri(@"/AzurLaneDatabase;component/Resources/chars/" + Name.Replace(' ', '_') + ".png", UriKind.Relative);
            }

            image.EndInit();
        }

        public bool Retrofit(bool retrofit) {
            if(!canRetrofit) {
                return false;
            }
            isRetrofit = true;
            canRetrofit = false;
            this.rarity = (Rarity)((int)this.rarity + 1);
            SetMaxStar();
            image = new BitmapImage();

            image.BeginInit();

            if(isRetrofit) {
                image.UriSource = new Uri(@"/AzurLaneDatabase;component/Resources/chars/" + Name.Replace(' ', '_') + "_Retrofit.png", UriKind.Relative);
            } else {
                image.UriSource = new Uri(@"/AzurLaneDatabase;component/Resources/chars/" + Name.Replace(' ', '_') + ".png", UriKind.Relative);
            }

            image.EndInit();

            charImage.Source = image;

            return true;
        }

        public void AddStar() {
            if(stars == maxStars) return;
            this.stars += 1;
        }

        public void RemoveStar() {
            if(stars <= 1) return;
            this.stars -= 1;
        }

        public void SetStars(int stars) {
            this.stars = stars;
        }

        public void ResetStars() {
            CheckStarLevel();
        }
    }
}
