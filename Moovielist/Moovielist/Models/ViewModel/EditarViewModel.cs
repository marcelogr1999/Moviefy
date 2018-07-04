using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moovielist.Models.ViewModel
{
    public class EditarViewModel
    {
        public EditarViewModel()
        {
            _item = new Item();

            ItemsInDropDown = new List<SelectListItem>(){
                new SelectListItem{ Text="-Selecione-", Selected=false, Value="0"},
                new SelectListItem{ Text="Assistindo", Selected=false, Value="1"},
                new SelectListItem{ Text="Para Depois", Selected=false, Value="2"},
                new SelectListItem{ Text="Terminado", Selected=false, Value="3"}
            };

        }

        public List<SelectListItem> ItemsInDropDown { get; set; }
        public Item _item { get; set; }
        public string SelectedValue { get; set; }

        public void Post()
        {
            if(_item.Estado.StartsWith("-"))
            {
                ItemsInDropDown.FirstOrDefault().Selected = true;
            }
            else
            {
                if (_item.Estado.StartsWith("A"))
                {
                    ItemsInDropDown[1].Selected = true;
                }
                else
                {
                    if (_item.Estado.StartsWith("P"))
                    {
                        ItemsInDropDown[2].Selected = true;
                    }
                    else
                    {
                        ItemsInDropDown[3].Selected = true;
                    }
                }
            }
        }
    }
}