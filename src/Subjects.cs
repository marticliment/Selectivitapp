using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace src
{
    public class Subject
    {
        public string Name { get; init; }
        public string Code { get; init; }
        public string IconPath { get; init; }
        public int MaxYear = 2022;
        public int MinYear = 2000;
    }

    public static class Subjects
    {
        public static IReadOnlyList<Subject> General = new List<Subject>()
        {
            new Subject(){
                Code="llca",
                IconPath="ms-appx:///Assets/a.png",
                Name="Català"
            },
            new Subject(){
                Code="lles",
                IconPath="ms-appx:///Assets/y.png",
                Name="Castellà"
            },
            new Subject(){
                Code="hist",
                IconPath="ms-appx:///Assets/history.png",
                Name="Història d'Espanya"
            },
            new Subject(){
                Code="hfil",
                IconPath="ms-appx:///Assets/scroll.png",
                Name="Història de la Filosofia"
            }
        };

        public static IReadOnlyList<Subject> Languages = new List<Subject>()
        { 
            new Subject(){
                Code="angl",
                IconPath="ms-appx:///Assets/bus.png",
                Name="Anglès" 
            },
            new Subject(){
                Code="fran",
                IconPath="ms-appx:///Assets/eiffel.png",
                Name="Francès" 
            },
            new Subject(){
                Code="alem",
                IconPath="ms-appx:///Assets/germany.png",
                Name="Alemany" 
            },
            new Subject(){
                Code="ital",
                IconPath="ms-appx:///Assets/pisa.png",
                Name="Italià" 
            },

        };

        public static IReadOnlyList<Subject> Science = new List<Subject>()
        {
            new Subject() {
                Code="biol",
                IconPath="ms-appx:///Assets/cell.png",
                Name="Biologia"
            },
            new Subject() {                                
                Code="dibu",
                IconPath="ms-appx:///Assets/tecnic.png",
                Name="Dibuix Tècnic" 
            },
            new Subject() {                                
                Code="cite",
                IconPath="ms-appx:///Assets/volcano.png",
                Name="Ciències de la Terra i del Medi Ambient" 
            },
            new Subject() {                                
                Code="elec",
                IconPath="ms-appx:///Assets/resistor.png",
                Name="Electrotècnia" 
            },
            new Subject() {                                
                Code="fisi",
                IconPath="ms-appx:///Assets/physics.png",
                Name="Física" 
            },
            new Subject() {                                
                Code="mate",
                IconPath="ms-appx:///Assets/sigma.png",
                Name="Matemàtiques" 
            },
            new Subject() {                                
                Code="tecn",
                IconPath="ms-appx:///Assets/crane.png",
                Name="Tecnologia Industrial" 
            },
            new Subject() {                                
                Code="quim",
                IconPath="ms-appx:///Assets/chem.png",
                Name="Química" 
            },
        };

        public static IReadOnlyList<Subject> Social = new List<Subject>()
        {
            new Subject() {                                
                Code="macs",
                IconPath="ms-appx:///Assets/graph.png",
                Name="Matemàtiques CC.SS." 
            },
            new Subject() {                                
                Code="ecem",
                IconPath="ms-appx:///Assets/sales.png",
                Name="Economia de l'Empresa" 
            },
            new Subject() {                                
                Code="geog",
                IconPath="ms-appx:///Assets/geography.png",
                Name="Geografia" 
            },
            new Subject() {                                
                Code="hart",
                IconPath="ms-appx:///Assets/bust.png",
                Name="Història de l'Art" 
            },
            new Subject() {                                
                Code="llat",
                IconPath="ms-appx:///Assets/latin.png",
                Name="Llatí" 
            },
            new Subject() {                                
                Code="grec",
                IconPath="ms-appx:///Assets/greek.png",
                Name="Grec" 
            },
            new Subject() {                                
                Code="lica",
                IconPath="ms-appx:///Assets/book.png",
                Name="Literatura Catalana" 
            },
            new Subject() {                                
                Code="lies",
                IconPath="ms-appx:///Assets/book2.png",
                Name="Literatura Castellana" 
            },
        };

        public static IReadOnlyList<Subject> Artistic = new List<Subject>()
        {
            new Subject() {                                
                Code="fart",
                IconPath="ms-appx:///Assets/arts.png",
                Name="Fonaments de les arts" 
            },
            new Subject() {                                
                Code="amus",
                IconPath="ms-appx:///Assets/music.png",
                Name="Anàlisi Musical" 
            },
            new Subject() {                                
                Code="diss",
                IconPath="ms-appx:///Assets/design.png",
                Name="Disseny" 
            },
            new Subject() {                                
                Code="cuau",
                IconPath="ms-appx:///Assets/audiovisual.png",
                Name="Cultura Audiovisual" 
            },
            new Subject() {                                
                Code="diar",
                IconPath="ms-appx:///Assets/palette.png",
                Name="Dibuix Artístic" 
            },
        };

    }
}
