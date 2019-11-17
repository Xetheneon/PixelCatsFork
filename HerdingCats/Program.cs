using System;
using System.Collections.Generic;
using System.Threading;
using PixelBoard;

namespace HerdingCats
{
    class Program
    {
        public enum State { Title, Playing, GameOver };
        public static State state = State.Playing;
        public static IPixel[,] board = new IPixel[20, 10];
        public static IPixel[,] background = new IPixel[20, 10];
        public static Random rng = new Random();

        public static IPixel[,] title = { { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(255, 8, 0), new Pixel(255, 12, 0), new Pixel(255, 18, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(255, 63, 0), new Pixel(255, 72, 0), new Pixel(255, 84, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(255, 114, 0), new Pixel(254, 123, 0), new Pixel(253, 131, 0), new Pixel(251, 142, 0), new Pixel(249, 150, 0), new Pixel(247, 159, 0), new Pixel(244, 170, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(236, 196, 0), new Pixel(233, 203, 0), new Pixel(231, 210, 0), new Pixel(227, 217, 0), new Pixel(224, 223, 0), new Pixel(222, 227, 0), new Pixel(218, 232, 0), new Pixel(216, 236, 0), new Pixel(213, 239, 0), new Pixel(210, 242, 0), new Pixel(206, 245, 0), new Pixel(203, 247, 0), new Pixel(199, 249, 0), new Pixel(195, 251, 0), new Pixel(190, 252, 0), new Pixel(184, 254, 0), new Pixel(178, 255, 0), new Pixel(171, 255, 0), new Pixel(163, 255, 0), new Pixel(155, 255, 0), new Pixel(146, 255, 0), new Pixel(135, 255, 0), new Pixel(126, 255, 0), new Pixel(117, 255, 0), new Pixel(106, 255, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(80, 255, 0), new Pixel(73, 254, 4), new Pixel(66, 253, 18), new Pixel(57, 252, 39), new Pixel(50, 251, 60), new Pixel(43, 250, 83), new Pixel(35, 248, 112), new Pixel(28, 246, 136), new Pixel(22, 245, 161), new Pixel(16, 242, 189), new Pixel(11, 239, 210), new Pixel(6, 236, 229), new Pixel(1, 232, 247), new Pixel(0, 228, 255), new Pixel(0, 223, 255), new Pixel(0, 217, 255), new Pixel(0, 211, 255), new Pixel(0, 204, 255), new Pixel(0, 196, 255), new Pixel(0, 189, 255), new Pixel(0, 182, 255), new Pixel(0, 174, 255), new Pixel(0, 167, 255), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 123, 255), new Pixel(0, 116, 255), new Pixel(0, 108, 255), new Pixel(0, 101, 255), new Pixel(0, 94, 255), new Pixel(0, 86, 255), new Pixel(0, 80, 255), new Pixel(0, 73, 255), new Pixel(0, 66, 255), new Pixel(1, 60, 255), new Pixel(4, 54, 255), new Pixel(8, 47, 255), new Pixel(11, 41, 255), new Pixel(15, 36, 255), new Pixel(21, 29, 255), new Pixel(26, 24, 255), new Pixel(32, 19, 255), new Pixel(40, 13, 255), new Pixel(47, 9, 255), new Pixel(55, 5, 255), new Pixel(66, 1, 255), new Pixel(76, 0, 251), new Pixel(88, 0, 243), new Pixel(105, 0, 231), new Pixel(120, 0, 219), new Pixel(137, 0, 206), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(210, 0, 146), new Pixel(225, 0, 134), new Pixel(237, 0, 123), new Pixel(250, 0, 113), new Pixel(255, 0, 107), new Pixel(255, 0, 103), new Pixel(255, 0, 100), new Pixel(255, 0, 99), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(255, 8, 0), new Pixel(255, 12, 0), new Pixel(255, 18, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(255, 63, 0), new Pixel(255, 72, 0), new Pixel(255, 84, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(255, 114, 0), new Pixel(254, 123, 0), new Pixel(253, 131, 0), new Pixel(251, 142, 0), new Pixel(249, 150, 0), new Pixel(247, 159, 0), new Pixel(244, 170, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(236, 196, 0), new Pixel(233, 203, 0), new Pixel(231, 210, 0), new Pixel(227, 217, 0), new Pixel(224, 223, 0), new Pixel(222, 227, 0), new Pixel(218, 232, 0), new Pixel(216, 236, 0), new Pixel(213, 239, 0), new Pixel(210, 242, 0), new Pixel(206, 245, 0), new Pixel(203, 247, 0), new Pixel(199, 249, 0), new Pixel(195, 251, 0), new Pixel(190, 252, 0), new Pixel(184, 254, 0), new Pixel(178, 255, 0), new Pixel(171, 255, 0), new Pixel(163, 255, 0), new Pixel(155, 255, 0), new Pixel(146, 255, 0), new Pixel(135, 255, 0), new Pixel(126, 255, 0), new Pixel(117, 255, 0), new Pixel(106, 255, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(80, 255, 0), new Pixel(73, 254, 4), new Pixel(66, 253, 18), new Pixel(57, 252, 39), new Pixel(50, 251, 60), new Pixel(43, 250, 83), new Pixel(35, 248, 112), new Pixel(28, 246, 136), new Pixel(22, 245, 161), new Pixel(16, 242, 189), new Pixel(11, 239, 210), new Pixel(6, 236, 229), new Pixel(1, 232, 247), new Pixel(0, 228, 255), new Pixel(0, 223, 255), new Pixel(0, 217, 255), new Pixel(0, 211, 255), new Pixel(0, 204, 255), new Pixel(0, 196, 255), new Pixel(0, 189, 255), new Pixel(0, 182, 255), new Pixel(0, 174, 255), new Pixel(0, 167, 255), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 123, 255), new Pixel(0, 116, 255), new Pixel(0, 108, 255), new Pixel(0, 101, 255), new Pixel(0, 94, 255), new Pixel(0, 86, 255), new Pixel(0, 80, 255), new Pixel(0, 73, 255), new Pixel(0, 66, 255), new Pixel(1, 60, 255), new Pixel(4, 54, 255), new Pixel(8, 47, 255), new Pixel(11, 41, 255), new Pixel(15, 36, 255), new Pixel(21, 29, 255), new Pixel(26, 24, 255), new Pixel(32, 19, 255), new Pixel(40, 13, 255), new Pixel(47, 9, 255), new Pixel(55, 5, 255), new Pixel(66, 1, 255), new Pixel(76, 0, 251), new Pixel(88, 0, 243), new Pixel(105, 0, 231), new Pixel(120, 0, 219), new Pixel(137, 0, 206), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(210, 0, 146), new Pixel(225, 0, 134), new Pixel(237, 0, 123), new Pixel(250, 0, 113), new Pixel(255, 0, 107), new Pixel(255, 0, 103), new Pixel(255, 0, 100), new Pixel(255, 0, 99), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(255, 8, 0), new Pixel(255, 12, 0), new Pixel(255, 18, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(255, 63, 0), new Pixel(255, 72, 0), new Pixel(255, 84, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(255, 114, 0), new Pixel(254, 123, 0), new Pixel(253, 131, 0), new Pixel(251, 142, 0), new Pixel(249, 150, 0), new Pixel(247, 159, 0), new Pixel(244, 170, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(236, 196, 0), new Pixel(233, 203, 0), new Pixel(231, 210, 0), new Pixel(227, 217, 0), new Pixel(224, 223, 0), new Pixel(222, 227, 0), new Pixel(218, 232, 0), new Pixel(216, 236, 0), new Pixel(213, 239, 0), new Pixel(210, 242, 0), new Pixel(206, 245, 0), new Pixel(203, 247, 0), new Pixel(199, 249, 0), new Pixel(195, 251, 0), new Pixel(190, 252, 0), new Pixel(184, 254, 0), new Pixel(178, 255, 0), new Pixel(171, 255, 0), new Pixel(163, 255, 0), new Pixel(155, 255, 0), new Pixel(146, 255, 0), new Pixel(135, 255, 0), new Pixel(126, 255, 0), new Pixel(117, 255, 0), new Pixel(106, 255, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(80, 255, 0), new Pixel(73, 254, 4), new Pixel(66, 253, 18), new Pixel(57, 252, 39), new Pixel(50, 251, 60), new Pixel(43, 250, 83), new Pixel(35, 248, 112), new Pixel(28, 246, 136), new Pixel(22, 245, 161), new Pixel(16, 242, 189), new Pixel(11, 239, 210), new Pixel(6, 236, 229), new Pixel(1, 232, 247), new Pixel(0, 228, 255), new Pixel(0, 223, 255), new Pixel(0, 217, 255), new Pixel(0, 211, 255), new Pixel(0, 204, 255), new Pixel(0, 196, 255), new Pixel(0, 189, 255), new Pixel(0, 182, 255), new Pixel(0, 174, 255), new Pixel(0, 167, 255), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 123, 255), new Pixel(0, 116, 255), new Pixel(0, 108, 255), new Pixel(0, 101, 255), new Pixel(0, 94, 255), new Pixel(0, 86, 255), new Pixel(0, 80, 255), new Pixel(0, 73, 255), new Pixel(0, 66, 255), new Pixel(1, 60, 255), new Pixel(4, 54, 255), new Pixel(8, 47, 255), new Pixel(11, 41, 255), new Pixel(15, 36, 255), new Pixel(21, 29, 255), new Pixel(26, 24, 255), new Pixel(32, 19, 255), new Pixel(40, 13, 255), new Pixel(47, 9, 255), new Pixel(55, 5, 255), new Pixel(66, 1, 255), new Pixel(76, 0, 251), new Pixel(88, 0, 243), new Pixel(105, 0, 231), new Pixel(120, 0, 219), new Pixel(137, 0, 206), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(210, 0, 146), new Pixel(225, 0, 134), new Pixel(237, 0, 123), new Pixel(250, 0, 113), new Pixel(255, 0, 107), new Pixel(255, 0, 103), new Pixel(255, 0, 100), new Pixel(255, 0, 99), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(255, 8, 0), new Pixel(255, 12, 0), new Pixel(255, 18, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(255, 63, 0), new Pixel(255, 72, 0), new Pixel(255, 84, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(255, 114, 0), new Pixel(254, 123, 0), new Pixel(253, 131, 0), new Pixel(251, 142, 0), new Pixel(249, 150, 0), new Pixel(247, 159, 0), new Pixel(244, 170, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(236, 196, 0), new Pixel(233, 203, 0), new Pixel(231, 210, 0), new Pixel(227, 217, 0), new Pixel(224, 223, 0), new Pixel(222, 227, 0), new Pixel(218, 232, 0), new Pixel(216, 236, 0), new Pixel(213, 239, 0), new Pixel(210, 242, 0), new Pixel(206, 245, 0), new Pixel(203, 247, 0), new Pixel(199, 249, 0), new Pixel(195, 251, 0), new Pixel(190, 252, 0), new Pixel(184, 254, 0), new Pixel(178, 255, 0), new Pixel(171, 255, 0), new Pixel(163, 255, 0), new Pixel(155, 255, 0), new Pixel(146, 255, 0), new Pixel(135, 255, 0), new Pixel(126, 255, 0), new Pixel(117, 255, 0), new Pixel(106, 255, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(80, 255, 0), new Pixel(73, 254, 4), new Pixel(66, 253, 18), new Pixel(57, 252, 39), new Pixel(50, 251, 60), new Pixel(43, 250, 83), new Pixel(35, 248, 112), new Pixel(28, 246, 136), new Pixel(22, 245, 161), new Pixel(16, 242, 189), new Pixel(11, 239, 210), new Pixel(6, 236, 229), new Pixel(1, 232, 247), new Pixel(0, 228, 255), new Pixel(0, 223, 255), new Pixel(0, 217, 255), new Pixel(0, 211, 255), new Pixel(0, 204, 255), new Pixel(0, 196, 255), new Pixel(0, 189, 255), new Pixel(0, 182, 255), new Pixel(0, 174, 255), new Pixel(0, 167, 255), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 123, 255), new Pixel(0, 116, 255), new Pixel(0, 108, 255), new Pixel(0, 101, 255), new Pixel(0, 94, 255), new Pixel(0, 86, 255), new Pixel(0, 80, 255), new Pixel(0, 73, 255), new Pixel(0, 66, 255), new Pixel(1, 60, 255), new Pixel(4, 54, 255), new Pixel(8, 47, 255), new Pixel(11, 41, 255), new Pixel(15, 36, 255), new Pixel(21, 29, 255), new Pixel(26, 24, 255), new Pixel(32, 19, 255), new Pixel(40, 13, 255), new Pixel(47, 9, 255), new Pixel(55, 5, 255), new Pixel(66, 1, 255), new Pixel(76, 0, 251), new Pixel(88, 0, 243), new Pixel(105, 0, 231), new Pixel(120, 0, 219), new Pixel(137, 0, 206), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(210, 0, 146), new Pixel(225, 0, 134), new Pixel(237, 0, 123), new Pixel(250, 0, 113), new Pixel(255, 0, 107), new Pixel(255, 0, 103), new Pixel(255, 0, 100), new Pixel(255, 0, 99), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(73, 2, 0), new Pixel(39, 1, 0), new Pixel(31, 2, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(63, 15, 0), new Pixel(38, 10, 0), new Pixel(137, 45, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(67, 29, 0), new Pixel(51, 25, 0), new Pixel(51, 26, 0), new Pixel(51, 28, 0), new Pixel(51, 31, 0), new Pixel(51, 33, 0), new Pixel(50, 35, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(79, 66, 0), new Pixel(47, 41, 0), new Pixel(48, 43, 0), new Pixel(47, 45, 0), new Pixel(45, 45, 0), new Pixel(83, 85, 0), new Pixel(124, 132, 0), new Pixel(216, 236, 0), new Pixel(213, 239, 0), new Pixel(210, 242, 0), new Pixel(168, 200, 0), new Pixel(41, 50, 0), new Pixel(40, 50, 0), new Pixel(39, 51, 0), new Pixel(41, 55, 0), new Pixel(64, 88, 0), new Pixel(85, 123, 0), new Pixel(129, 193, 0), new Pixel(163, 255, 0), new Pixel(155, 255, 0), new Pixel(146, 255, 0), new Pixel(135, 255, 0), new Pixel(54, 110, 0), new Pixel(17, 39, 0), new Pixel(4, 11, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(48, 156, 0), new Pixel(11, 40, 0), new Pixel(11, 42, 3), new Pixel(24, 106, 16), new Pixel(50, 251, 60), new Pixel(43, 250, 83), new Pixel(35, 248, 112), new Pixel(28, 246, 136), new Pixel(4, 48, 31), new Pixel(2, 39, 31), new Pixel(9, 209, 183), new Pixel(6, 236, 229), new Pixel(1, 232, 247), new Pixel(0, 228, 255), new Pixel(0, 223, 255), new Pixel(0, 108, 127), new Pixel(0, 53, 65), new Pixel(0, 22, 28), new Pixel(0, 5, 7), new Pixel(0, 21, 29), new Pixel(0, 62, 87), new Pixel(0, 105, 154), new Pixel(0, 167, 255), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 123, 255), new Pixel(0, 116, 255), new Pixel(0, 107, 253), new Pixel(0, 40, 102), new Pixel(0, 20, 56), new Pixel(0, 4, 14), new Pixel(0, 8, 28), new Pixel(0, 21, 75), new Pixel(0, 50, 194), new Pixel(1, 60, 255), new Pixel(4, 54, 255), new Pixel(8, 47, 255), new Pixel(11, 41, 255), new Pixel(9, 22, 159), new Pixel(3, 4, 40), new Pixel(3, 3, 36), new Pixel(15, 9, 124), new Pixel(40, 13, 255), new Pixel(47, 9, 255), new Pixel(33, 3, 155), new Pixel(16, 0, 64), new Pixel(19, 0, 62), new Pixel(22, 0, 60), new Pixel(26, 0, 57), new Pixel(30, 0, 54), new Pixel(34, 0, 51), new Pixel(39, 0, 47), new Pixel(43, 0, 44), new Pixel(140, 0, 118), new Pixel(210, 0, 146), new Pixel(173, 0, 103), new Pixel(75, 0, 39), new Pixel(24, 0, 11), new Pixel(14, 0, 5), new Pixel(54, 0, 21), new Pixel(130, 0, 50), new Pixel(255, 0, 99), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(73, 2, 0), new Pixel(0, 0, 0), new Pixel(82, 5, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(29, 7, 0), new Pixel(0, 0, 0), new Pixel(124, 40, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(13, 5, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(234, 172, 0), new Pixel(239, 186, 0), new Pixel(37, 30, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(70, 76, 0), new Pixel(213, 239, 0), new Pixel(210, 242, 0), new Pixel(138, 165, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(74, 116, 0), new Pixel(155, 255, 0), new Pixel(146, 255, 0), new Pixel(135, 255, 0), new Pixel(55, 113, 0), new Pixel(0, 0, 0), new Pixel(20, 49, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(32, 105, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(32, 165, 39), new Pixel(43, 250, 83), new Pixel(35, 248, 112), new Pixel(28, 246, 136), new Pixel(0, 2, 1), new Pixel(0, 0, 0), new Pixel(9, 209, 183), new Pixel(6, 236, 229), new Pixel(1, 232, 247), new Pixel(0, 171, 192), new Pixel(0, 39, 45), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 137, 210), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 123, 255), new Pixel(0, 91, 201), new Pixel(0, 17, 42), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 8, 32), new Pixel(1, 60, 255), new Pixel(4, 54, 255), new Pixel(8, 47, 255), new Pixel(11, 41, 255), new Pixel(2, 5, 36), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(1, 0, 8), new Pixel(38, 12, 243), new Pixel(47, 9, 255), new Pixel(22, 2, 104), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(119, 0, 101), new Pixel(154, 0, 107), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(7, 0, 2), new Pixel(255, 0, 99), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(73, 2, 0), new Pixel(0, 0, 0), new Pixel(74, 5, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(20, 4, 0), new Pixel(0, 0, 0), new Pixel(124, 40, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(13, 5, 0), new Pixel(0, 0, 0), new Pixel(116, 60, 0), new Pixel(229, 129, 0), new Pixel(227, 137, 0), new Pixel(225, 145, 0), new Pixel(224, 156, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(37, 30, 0), new Pixel(0, 0, 0), new Pixel(87, 79, 0), new Pixel(207, 198, 0), new Pixel(155, 154, 0), new Pixel(70, 72, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(177, 199, 0), new Pixel(210, 242, 0), new Pixel(138, 165, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(169, 218, 0), new Pixel(169, 225, 0), new Pixel(138, 191, 0), new Pixel(60, 87, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(81, 134, 0), new Pixel(146, 255, 0), new Pixel(135, 255, 0), new Pixel(55, 113, 0), new Pixel(0, 0, 0), new Pixel(20, 49, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(32, 105, 0), new Pixel(0, 0, 0), new Pixel(0, 2, 0), new Pixel(0, 0, 0), new Pixel(10, 50, 12), new Pixel(43, 250, 83), new Pixel(35, 248, 112), new Pixel(28, 246, 136), new Pixel(0, 2, 1), new Pixel(0, 0, 0), new Pixel(9, 209, 183), new Pixel(6, 236, 229), new Pixel(1, 232, 247), new Pixel(0, 55, 62), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 109, 132), new Pixel(0, 176, 220), new Pixel(0, 192, 251), new Pixel(0, 168, 228), new Pixel(0, 109, 153), new Pixel(0, 25, 37), new Pixel(0, 138, 211), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 123, 255), new Pixel(0, 33, 74), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 56, 153), new Pixel(0, 78, 232), new Pixel(0, 73, 235), new Pixel(0, 43, 153), new Pixel(0, 0, 1), new Pixel(1, 60, 255), new Pixel(4, 54, 255), new Pixel(8, 47, 255), new Pixel(8, 31, 199), new Pixel(0, 0, 0), new Pixel(0, 1, 12), new Pixel(1, 1, 12), new Pixel(0, 0, 0), new Pixel(24, 7, 155), new Pixel(47, 9, 255), new Pixel(51, 4, 239), new Pixel(57, 0, 223), new Pixel(66, 0, 219), new Pixel(57, 0, 159), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(104, 0, 157), new Pixel(137, 0, 166), new Pixel(152, 0, 153), new Pixel(185, 0, 156), new Pixel(80, 0, 56), new Pixel(0, 0, 0), new Pixel(13, 0, 6), new Pixel(241, 0, 109), new Pixel(255, 0, 107), new Pixel(220, 0, 88), new Pixel(119, 0, 46), new Pixel(255, 0, 99), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(73, 2, 0), new Pixel(0, 0, 0), new Pixel(66, 4, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(11, 2, 0), new Pixel(0, 0, 0), new Pixel(124, 40, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(13, 5, 0), new Pixel(0, 0, 0), new Pixel(126, 65, 0), new Pixel(251, 142, 0), new Pixel(249, 150, 0), new Pixel(247, 159, 0), new Pixel(244, 170, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(37, 31, 0), new Pixel(0, 0, 0), new Pixel(87, 79, 0), new Pixel(227, 217, 0), new Pixel(224, 223, 0), new Pixel(155, 159, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(159, 179, 0), new Pixel(210, 242, 0), new Pixel(138, 165, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(185, 239, 0), new Pixel(190, 252, 0), new Pixel(184, 254, 0), new Pixel(178, 255, 0), new Pixel(53, 80, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(124, 217, 0), new Pixel(135, 255, 0), new Pixel(55, 113, 0), new Pixel(0, 0, 0), new Pixel(20, 49, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(32, 105, 0), new Pixel(0, 0, 0), new Pixel(21, 82, 5), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(27, 157, 52), new Pixel(35, 248, 112), new Pixel(28, 246, 136), new Pixel(0, 2, 1), new Pixel(0, 0, 0), new Pixel(9, 209, 183), new Pixel(6, 236, 229), new Pixel(0, 146, 155), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 135, 159), new Pixel(0, 211, 255), new Pixel(0, 204, 255), new Pixel(0, 196, 255), new Pixel(0, 189, 255), new Pixel(0, 182, 255), new Pixel(0, 174, 255), new Pixel(0, 167, 255), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 83, 173), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 55, 141), new Pixel(0, 94, 255), new Pixel(0, 86, 255), new Pixel(0, 80, 255), new Pixel(0, 73, 255), new Pixel(0, 66, 255), new Pixel(1, 60, 255), new Pixel(4, 54, 255), new Pixel(8, 47, 255), new Pixel(4, 18, 112), new Pixel(0, 0, 0), new Pixel(7, 10, 92), new Pixel(9, 8, 92), new Pixel(0, 0, 0), new Pixel(10, 3, 68), new Pixel(47, 9, 255), new Pixel(55, 5, 255), new Pixel(66, 1, 255), new Pixel(76, 0, 251), new Pixel(65, 0, 182), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(119, 0, 180), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(55, 0, 38), new Pixel(0, 0, 0), new Pixel(59, 0, 30), new Pixel(250, 0, 113), new Pixel(255, 0, 107), new Pixel(255, 0, 103), new Pixel(255, 0, 100), new Pixel(255, 0, 99), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(73, 2, 0), new Pixel(0, 0, 0), new Pixel(61, 4, 0), new Pixel(219, 22, 0), new Pixel(219, 28, 0), new Pixel(219, 36, 0), new Pixel(219, 45, 0), new Pixel(2, 0, 0), new Pixel(0, 0, 0), new Pixel(124, 40, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(13, 5, 0), new Pixel(0, 0, 0), new Pixel(117, 60, 0), new Pixel(233, 131, 0), new Pixel(231, 139, 0), new Pixel(229, 147, 0), new Pixel(244, 170, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(37, 31, 0), new Pixel(0, 0, 0), new Pixel(87, 79, 0), new Pixel(227, 217, 0), new Pixel(224, 223, 0), new Pixel(126, 129, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(182, 205, 0), new Pixel(210, 242, 0), new Pixel(138, 165, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(185, 239, 0), new Pixel(190, 252, 0), new Pixel(184, 254, 0), new Pixel(178, 255, 0), new Pixel(101, 151, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(102, 179, 0), new Pixel(135, 255, 0), new Pixel(55, 113, 0), new Pixel(0, 0, 0), new Pixel(20, 49, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(32, 105, 0), new Pixel(0, 0, 0), new Pixel(30, 118, 8), new Pixel(17, 79, 12), new Pixel(0, 0, 0), new Pixel(7, 44, 14), new Pixel(35, 248, 112), new Pixel(28, 246, 136), new Pixel(0, 2, 1), new Pixel(0, 0, 0), new Pixel(9, 209, 183), new Pixel(6, 236, 229), new Pixel(0, 98, 104), new Pixel(0, 0, 0), new Pixel(0, 40, 46), new Pixel(0, 217, 255), new Pixel(0, 211, 255), new Pixel(0, 204, 255), new Pixel(0, 196, 255), new Pixel(0, 189, 255), new Pixel(0, 182, 255), new Pixel(0, 174, 255), new Pixel(0, 167, 255), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 62, 129), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 101, 255), new Pixel(0, 94, 255), new Pixel(0, 86, 255), new Pixel(0, 80, 255), new Pixel(0, 73, 255), new Pixel(0, 66, 255), new Pixel(1, 60, 255), new Pixel(4, 54, 255), new Pixel(8, 47, 255), new Pixel(1, 4, 28), new Pixel(0, 0, 0), new Pixel(14, 19, 175), new Pixel(17, 16, 175), new Pixel(0, 0, 0), new Pixel(0, 0, 4), new Pixel(43, 8, 235), new Pixel(55, 5, 255), new Pixel(66, 1, 255), new Pixel(76, 0, 251), new Pixel(65, 0, 182), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(119, 0, 180), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(72, 0, 50), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(93, 0, 42), new Pixel(205, 0, 86), new Pixel(255, 0, 103), new Pixel(255, 0, 100), new Pixel(255, 0, 99), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(73, 2, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(124, 40, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(13, 5, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(143, 100, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(37, 31, 0), new Pixel(0, 0, 0), new Pixel(31, 28, 0), new Pixel(97, 93, 0), new Pixel(72, 71, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(82, 89, 0), new Pixel(213, 239, 0), new Pixel(210, 242, 0), new Pixel(138, 165, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(185, 239, 0), new Pixel(190, 252, 0), new Pixel(184, 254, 0), new Pixel(178, 255, 0), new Pixel(158, 236, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(80, 141, 0), new Pixel(135, 255, 0), new Pixel(55, 113, 0), new Pixel(0, 0, 0), new Pixel(20, 49, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(32, 105, 0), new Pixel(0, 0, 0), new Pixel(30, 118, 8), new Pixel(42, 188, 29), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(21, 150, 68), new Pixel(28, 246, 136), new Pixel(0, 2, 1), new Pixel(0, 0, 0), new Pixel(9, 209, 183), new Pixel(6, 236, 229), new Pixel(0, 53, 57), new Pixel(0, 0, 0), new Pixel(0, 73, 84), new Pixel(0, 217, 255), new Pixel(0, 211, 255), new Pixel(0, 19, 24), new Pixel(0, 43, 56), new Pixel(0, 41, 56), new Pixel(0, 39, 56), new Pixel(0, 40, 59), new Pixel(0, 167, 255), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 45, 94), new Pixel(0, 0, 0), new Pixel(0, 8, 19), new Pixel(0, 101, 255), new Pixel(0, 94, 255), new Pixel(0, 86, 255), new Pixel(0, 80, 255), new Pixel(0, 73, 255), new Pixel(0, 66, 255), new Pixel(1, 60, 255), new Pixel(4, 54, 255), new Pixel(6, 35, 195), new Pixel(0, 0, 0), new Pixel(0, 1, 12), new Pixel(20, 28, 247), new Pixel(25, 23, 247), new Pixel(1, 0, 12), new Pixel(0, 0, 0), new Pixel(26, 5, 143), new Pixel(55, 5, 255), new Pixel(66, 1, 255), new Pixel(76, 0, 251), new Pixel(65, 0, 182), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(119, 0, 180), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(177, 0, 123), new Pixel(22, 0, 13), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(72, 0, 29), new Pixel(212, 0, 83), new Pixel(255, 0, 99), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(73, 2, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(124, 40, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(13, 5, 0), new Pixel(0, 0, 0), new Pixel(16, 8, 0), new Pixel(26, 15, 0), new Pixel(35, 21, 0), new Pixel(44, 28, 0), new Pixel(163, 114, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(37, 31, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(92, 98, 0), new Pixel(216, 236, 0), new Pixel(213, 239, 0), new Pixel(210, 242, 0), new Pixel(138, 165, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(185, 239, 0), new Pixel(190, 252, 0), new Pixel(184, 254, 0), new Pixel(178, 255, 0), new Pixel(162, 242, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(86, 151, 0), new Pixel(135, 255, 0), new Pixel(55, 113, 0), new Pixel(0, 0, 0), new Pixel(20, 49, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(33, 106, 0), new Pixel(0, 0, 0), new Pixel(30, 118, 8), new Pixel(57, 252, 39), new Pixel(16, 81, 19), new Pixel(0, 0, 0), new Pixel(2, 19, 8), new Pixel(26, 231, 128), new Pixel(0, 2, 1), new Pixel(0, 0, 0), new Pixel(9, 209, 183), new Pixel(6, 236, 229), new Pixel(0, 30, 32), new Pixel(0, 0, 0), new Pixel(0, 79, 91), new Pixel(0, 217, 255), new Pixel(0, 211, 255), new Pixel(0, 51, 64), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 137, 210), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 37, 77), new Pixel(0, 0, 0), new Pixel(0, 7, 18), new Pixel(0, 101, 255), new Pixel(0, 94, 255), new Pixel(0, 86, 255), new Pixel(0, 80, 255), new Pixel(0, 73, 255), new Pixel(0, 66, 255), new Pixel(1, 60, 255), new Pixel(4, 54, 255), new Pixel(3, 18, 100), new Pixel(0, 0, 0), new Pixel(5, 12, 92), new Pixel(21, 29, 255), new Pixel(26, 24, 255), new Pixel(11, 6, 92), new Pixel(0, 0, 0), new Pixel(11, 2, 60), new Pixel(55, 5, 255), new Pixel(66, 1, 255), new Pixel(76, 0, 251), new Pixel(65, 0, 182), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(119, 0, 180), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(210, 0, 146), new Pixel(196, 0, 117), new Pixel(97, 0, 50), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(17, 0, 6), new Pixel(224, 0, 86), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(73, 2, 0), new Pixel(0, 0, 0), new Pixel(68, 4, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(3, 0, 0), new Pixel(0, 0, 0), new Pixel(124, 40, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(13, 5, 0), new Pixel(0, 0, 0), new Pixel(126, 65, 0), new Pixel(251, 142, 0), new Pixel(249, 150, 0), new Pixel(247, 159, 0), new Pixel(244, 170, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(38, 32, 0), new Pixel(0, 0, 0), new Pixel(72, 65, 0), new Pixel(178, 170, 0), new Pixel(82, 82, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(166, 181, 0), new Pixel(213, 239, 0), new Pixel(210, 242, 0), new Pixel(138, 165, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(185, 239, 0), new Pixel(190, 252, 0), new Pixel(184, 254, 0), new Pixel(178, 255, 0), new Pixel(138, 206, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(112, 197, 0), new Pixel(135, 255, 0), new Pixel(55, 113, 0), new Pixel(0, 0, 0), new Pixel(20, 49, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(33, 106, 0), new Pixel(0, 0, 0), new Pixel(30, 118, 8), new Pixel(57, 252, 39), new Pixel(39, 197, 47), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(14, 130, 72), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(9, 209, 183), new Pixel(6, 236, 229), new Pixel(0, 65, 69), new Pixel(0, 0, 0), new Pixel(0, 47, 54), new Pixel(0, 217, 255), new Pixel(0, 211, 255), new Pixel(0, 190, 238), new Pixel(0, 176, 229), new Pixel(0, 132, 179), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 137, 210), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 54, 113), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 94, 238), new Pixel(0, 94, 255), new Pixel(0, 86, 255), new Pixel(0, 80, 255), new Pixel(0, 73, 255), new Pixel(0, 66, 255), new Pixel(1, 60, 255), new Pixel(3, 53, 251), new Pixel(0, 3, 20), new Pixel(0, 0, 0), new Pixel(4, 10, 76), new Pixel(10, 14, 127), new Pixel(12, 11, 127), new Pixel(9, 5, 76), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(48, 4, 227), new Pixel(66, 1, 255), new Pixel(76, 0, 251), new Pixel(65, 0, 182), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(119, 0, 180), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(210, 0, 146), new Pixel(225, 0, 134), new Pixel(237, 0, 123), new Pixel(201, 0, 91), new Pixel(44, 0, 18), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(120, 0, 46), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(73, 2, 0), new Pixel(0, 0, 0), new Pixel(77, 5, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(10, 2, 0), new Pixel(0, 0, 0), new Pixel(124, 40, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(12, 5, 0), new Pixel(0, 0, 0), new Pixel(126, 65, 0), new Pixel(251, 142, 0), new Pixel(249, 150, 0), new Pixel(247, 159, 0), new Pixel(244, 170, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(38, 32, 0), new Pixel(0, 0, 0), new Pixel(99, 90, 0), new Pixel(227, 217, 0), new Pixel(223, 222, 0), new Pixel(15, 16, 0), new Pixel(0, 0, 0), new Pixel(90, 99, 0), new Pixel(213, 239, 0), new Pixel(210, 242, 0), new Pixel(138, 165, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(185, 239, 0), new Pixel(190, 252, 0), new Pixel(184, 254, 0), new Pixel(178, 255, 0), new Pixel(71, 107, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(139, 243, 0), new Pixel(135, 255, 0), new Pixel(55, 113, 0), new Pixel(0, 0, 0), new Pixel(20, 49, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(33, 106, 0), new Pixel(0, 0, 0), new Pixel(30, 118, 8), new Pixel(57, 252, 39), new Pixel(50, 251, 60), new Pixel(15, 91, 30), new Pixel(0, 0, 0), new Pixel(1, 9, 5), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(9, 209, 183), new Pixel(6, 236, 229), new Pixel(0, 99, 105), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 166, 196), new Pixel(0, 211, 255), new Pixel(0, 204, 255), new Pixel(0, 196, 255), new Pixel(0, 147, 199), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 137, 210), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 71, 149), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 65, 165), new Pixel(0, 94, 255), new Pixel(0, 86, 255), new Pixel(0, 80, 255), new Pixel(0, 73, 255), new Pixel(0, 66, 255), new Pixel(1, 60, 255), new Pixel(2, 39, 187), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(29, 2, 135), new Pixel(66, 1, 255), new Pixel(76, 0, 251), new Pixel(65, 0, 182), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(119, 0, 180), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(210, 0, 146), new Pixel(225, 0, 134), new Pixel(237, 0, 123), new Pixel(250, 0, 113), new Pixel(255, 0, 107), new Pixel(32, 0, 12), new Pixel(0, 0, 0), new Pixel(74, 0, 28), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(73, 2, 0), new Pixel(0, 0, 0), new Pixel(85, 6, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(17, 4, 0), new Pixel(0, 0, 0), new Pixel(124, 40, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(12, 5, 0), new Pixel(0, 0, 0), new Pixel(126, 65, 0), new Pixel(251, 142, 0), new Pixel(249, 150, 0), new Pixel(247, 159, 0), new Pixel(244, 170, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(38, 32, 0), new Pixel(0, 0, 0), new Pixel(107, 98, 0), new Pixel(227, 217, 0), new Pixel(224, 223, 0), new Pixel(103, 105, 0), new Pixel(0, 0, 0), new Pixel(8, 9, 0), new Pixel(202, 227, 0), new Pixel(210, 242, 0), new Pixel(138, 165, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(185, 239, 0), new Pixel(190, 252, 0), new Pixel(163, 226, 0), new Pixel(83, 120, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(74, 123, 0), new Pixel(146, 255, 0), new Pixel(135, 255, 0), new Pixel(55, 113, 0), new Pixel(0, 0, 0), new Pixel(20, 49, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(33, 106, 0), new Pixel(0, 0, 0), new Pixel(30, 118, 8), new Pixel(57, 252, 39), new Pixel(50, 251, 60), new Pixel(35, 206, 68), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(9, 209, 183), new Pixel(6, 236, 229), new Pixel(0, 210, 223), new Pixel(0, 15, 17), new Pixel(0, 0, 0), new Pixel(0, 45, 53), new Pixel(0, 162, 196), new Pixel(0, 204, 255), new Pixel(0, 196, 255), new Pixel(0, 134, 182), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 137, 210), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 113, 236), new Pixel(0, 4, 9), new Pixel(0, 0, 0), new Pixel(0, 19, 48), new Pixel(0, 75, 205), new Pixel(0, 86, 255), new Pixel(0, 80, 255), new Pixel(0, 58, 206), new Pixel(0, 20, 78), new Pixel(1, 60, 255), new Pixel(1, 20, 96), new Pixel(0, 0, 0), new Pixel(2, 9, 56), new Pixel(11, 26, 191), new Pixel(15, 21, 191), new Pixel(19, 17, 191), new Pixel(23, 14, 191), new Pixel(8, 2, 56), new Pixel(0, 0, 0), new Pixel(10, 0, 48), new Pixel(66, 1, 255), new Pixel(76, 0, 251), new Pixel(65, 0, 182), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(119, 0, 180), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(79, 0, 54), new Pixel(182, 0, 108), new Pixel(237, 0, 123), new Pixel(250, 0, 113), new Pixel(205, 0, 86), new Pixel(22, 0, 8), new Pixel(0, 0, 0), new Pixel(132, 0, 51), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(73, 2, 0), new Pixel(0, 0, 0), new Pixel(90, 6, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(24, 5, 0), new Pixel(0, 0, 0), new Pixel(124, 40, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(12, 5, 0), new Pixel(0, 0, 0), new Pixel(8, 4, 0), new Pixel(17, 10, 0), new Pixel(26, 15, 0), new Pixel(0, 0, 0), new Pixel(1, 1, 0), new Pixel(241, 177, 0), new Pixel(239, 186, 0), new Pixel(39, 33, 0), new Pixel(0, 0, 0), new Pixel(108, 98, 0), new Pixel(227, 217, 0), new Pixel(224, 223, 0), new Pixel(191, 195, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(116, 131, 0), new Pixel(210, 242, 0), new Pixel(138, 164, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(19, 24, 0), new Pixel(23, 30, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(44, 69, 0), new Pixel(155, 255, 0), new Pixel(146, 255, 0), new Pixel(135, 255, 0), new Pixel(55, 113, 0), new Pixel(0, 0, 0), new Pixel(20, 49, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(33, 106, 0), new Pixel(0, 0, 0), new Pixel(30, 118, 8), new Pixel(57, 252, 39), new Pixel(50, 251, 60), new Pixel(43, 250, 83), new Pixel(14, 102, 46), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(9, 209, 183), new Pixel(6, 236, 229), new Pixel(1, 232, 247), new Pixel(0, 143, 160), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 137, 210), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 123, 255), new Pixel(0, 58, 129), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 1, 6), new Pixel(0, 58, 247), new Pixel(0, 2, 12), new Pixel(0, 0, 0), new Pixel(6, 22, 143), new Pixel(15, 36, 255), new Pixel(21, 29, 255), new Pixel(26, 24, 255), new Pixel(32, 19, 255), new Pixel(24, 7, 155), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(56, 0, 219), new Pixel(76, 0, 251), new Pixel(65, 0, 182), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(119, 0, 180), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(37, 0, 19), new Pixel(74, 0, 33), new Pixel(9, 0, 3), new Pixel(0, 0, 0), new Pixel(10, 0, 3), new Pixel(221, 0, 85), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(73, 2, 0), new Pixel(0, 0, 0), new Pixel(90, 6, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(124, 40, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(47, 21, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(234, 172, 0), new Pixel(239, 186, 0), new Pixel(39, 33, 0), new Pixel(0, 0, 0), new Pixel(108, 98, 0), new Pixel(227, 217, 0), new Pixel(224, 223, 0), new Pixel(222, 227, 0), new Pixel(71, 76, 0), new Pixel(0, 0, 0), new Pixel(24, 27, 0), new Pixel(210, 242, 0), new Pixel(155, 184, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(4, 5, 0), new Pixel(28, 39, 0), new Pixel(51, 74, 0), new Pixel(112, 168, 0), new Pixel(163, 255, 0), new Pixel(155, 255, 0), new Pixel(146, 255, 0), new Pixel(135, 255, 0), new Pixel(55, 113, 0), new Pixel(0, 0, 0), new Pixel(20, 49, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(33, 107, 0), new Pixel(0, 0, 0), new Pixel(30, 118, 8), new Pixel(57, 252, 39), new Pixel(50, 251, 60), new Pixel(43, 250, 83), new Pixel(33, 239, 108), new Pixel(4, 39, 21), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(9, 209, 184), new Pixel(6, 236, 229), new Pixel(1, 232, 247), new Pixel(0, 228, 255), new Pixel(0, 183, 210), new Pixel(0, 57, 67), new Pixel(0, 6, 8), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 12, 17), new Pixel(0, 62, 92), new Pixel(0, 167, 255), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 123, 255), new Pixel(0, 116, 255), new Pixel(0, 74, 175), new Pixel(0, 14, 36), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 10, 38), new Pixel(0, 35, 136), new Pixel(0, 45, 195), new Pixel(0, 0, 0), new Pixel(0, 0, 4), new Pixel(9, 36, 227), new Pixel(15, 36, 255), new Pixel(21, 29, 255), new Pixel(26, 24, 255), new Pixel(32, 19, 255), new Pixel(36, 11, 235), new Pixel(2, 0, 12), new Pixel(0, 0, 0), new Pixel(37, 0, 143), new Pixel(76, 0, 251), new Pixel(67, 0, 185), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(121, 0, 183), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(106, 0, 73), new Pixel(29, 0, 17), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(73, 0, 29), new Pixel(171, 0, 67), new Pixel(255, 0, 99), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(255, 8, 0), new Pixel(232, 10, 0), new Pixel(255, 18, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(255, 63, 0), new Pixel(240, 67, 0), new Pixel(255, 84, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(255, 114, 0), new Pixel(247, 119, 0), new Pixel(246, 127, 0), new Pixel(244, 138, 0), new Pixel(242, 145, 0), new Pixel(240, 154, 0), new Pixel(237, 165, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(236, 196, 0), new Pixel(226, 197, 0), new Pixel(231, 210, 0), new Pixel(227, 217, 0), new Pixel(224, 223, 0), new Pixel(222, 227, 0), new Pixel(218, 232, 0), new Pixel(210, 229, 0), new Pixel(207, 232, 0), new Pixel(210, 242, 0), new Pixel(206, 245, 0), new Pixel(197, 240, 0), new Pixel(193, 242, 0), new Pixel(189, 244, 0), new Pixel(190, 252, 0), new Pixel(184, 254, 0), new Pixel(178, 255, 0), new Pixel(171, 255, 0), new Pixel(163, 255, 0), new Pixel(155, 255, 0), new Pixel(146, 255, 0), new Pixel(135, 255, 0), new Pixel(126, 255, 0), new Pixel(108, 237, 0), new Pixel(106, 255, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(80, 255, 0), new Pixel(65, 227, 3), new Pixel(66, 253, 18), new Pixel(57, 252, 39), new Pixel(50, 251, 60), new Pixel(43, 250, 83), new Pixel(35, 248, 112), new Pixel(28, 246, 136), new Pixel(22, 245, 161), new Pixel(16, 242, 189), new Pixel(11, 239, 210), new Pixel(6, 236, 229), new Pixel(1, 232, 247), new Pixel(0, 228, 255), new Pixel(0, 223, 255), new Pixel(0, 217, 255), new Pixel(0, 211, 255), new Pixel(0, 179, 224), new Pixel(0, 156, 204), new Pixel(0, 160, 217), new Pixel(0, 182, 255), new Pixel(0, 174, 255), new Pixel(0, 167, 255), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 123, 255), new Pixel(0, 116, 255), new Pixel(0, 108, 255), new Pixel(0, 101, 255), new Pixel(0, 89, 244), new Pixel(0, 68, 202), new Pixel(0, 71, 227), new Pixel(0, 73, 255), new Pixel(0, 66, 255), new Pixel(1, 60, 255), new Pixel(4, 54, 255), new Pixel(8, 47, 255), new Pixel(11, 41, 255), new Pixel(15, 36, 255), new Pixel(21, 29, 255), new Pixel(26, 24, 255), new Pixel(32, 19, 255), new Pixel(40, 13, 255), new Pixel(47, 9, 255), new Pixel(55, 5, 255), new Pixel(66, 1, 255), new Pixel(76, 0, 251), new Pixel(88, 0, 243), new Pixel(105, 0, 231), new Pixel(120, 0, 219), new Pixel(137, 0, 206), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(210, 0, 146), new Pixel(225, 0, 134), new Pixel(213, 0, 110), new Pixel(205, 0, 93), new Pixel(249, 0, 104), new Pixel(255, 0, 103), new Pixel(255, 0, 100), new Pixel(255, 0, 99), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(255, 8, 0), new Pixel(255, 12, 0), new Pixel(255, 18, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(255, 63, 0), new Pixel(255, 72, 0), new Pixel(255, 84, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(255, 114, 0), new Pixel(254, 123, 0), new Pixel(253, 131, 0), new Pixel(251, 142, 0), new Pixel(249, 150, 0), new Pixel(247, 159, 0), new Pixel(244, 170, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(236, 196, 0), new Pixel(233, 203, 0), new Pixel(231, 210, 0), new Pixel(227, 217, 0), new Pixel(224, 223, 0), new Pixel(222, 227, 0), new Pixel(218, 232, 0), new Pixel(216, 236, 0), new Pixel(213, 239, 0), new Pixel(210, 242, 0), new Pixel(206, 245, 0), new Pixel(203, 247, 0), new Pixel(199, 249, 0), new Pixel(195, 251, 0), new Pixel(190, 252, 0), new Pixel(184, 254, 0), new Pixel(178, 255, 0), new Pixel(171, 255, 0), new Pixel(163, 255, 0), new Pixel(155, 255, 0), new Pixel(146, 255, 0), new Pixel(135, 255, 0), new Pixel(126, 255, 0), new Pixel(117, 255, 0), new Pixel(106, 255, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(80, 255, 0), new Pixel(73, 254, 4), new Pixel(66, 253, 18), new Pixel(57, 252, 39), new Pixel(50, 251, 60), new Pixel(43, 250, 83), new Pixel(35, 248, 112), new Pixel(28, 246, 136), new Pixel(22, 245, 161), new Pixel(16, 242, 189), new Pixel(11, 239, 210), new Pixel(6, 236, 229), new Pixel(1, 232, 247), new Pixel(0, 228, 255), new Pixel(0, 223, 255), new Pixel(0, 217, 255), new Pixel(0, 211, 255), new Pixel(0, 204, 255), new Pixel(0, 196, 255), new Pixel(0, 189, 255), new Pixel(0, 182, 255), new Pixel(0, 174, 255), new Pixel(0, 167, 255), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 123, 255), new Pixel(0, 116, 255), new Pixel(0, 108, 255), new Pixel(0, 101, 255), new Pixel(0, 94, 255), new Pixel(0, 86, 255), new Pixel(0, 80, 255), new Pixel(0, 73, 255), new Pixel(0, 66, 255), new Pixel(1, 60, 255), new Pixel(4, 54, 255), new Pixel(8, 47, 255), new Pixel(11, 41, 255), new Pixel(15, 36, 255), new Pixel(21, 29, 255), new Pixel(26, 24, 255), new Pixel(32, 19, 255), new Pixel(40, 13, 255), new Pixel(47, 9, 255), new Pixel(55, 5, 255), new Pixel(66, 1, 255), new Pixel(76, 0, 251), new Pixel(88, 0, 243), new Pixel(105, 0, 231), new Pixel(120, 0, 219), new Pixel(137, 0, 206), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(210, 0, 146), new Pixel(225, 0, 134), new Pixel(237, 0, 123), new Pixel(250, 0, 113), new Pixel(255, 0, 107), new Pixel(255, 0, 103), new Pixel(255, 0, 100), new Pixel(255, 0, 99), new Pixel(255, 0, 99), }, { new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(255, 8, 0), new Pixel(255, 12, 0), new Pixel(255, 18, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(255, 63, 0), new Pixel(255, 72, 0), new Pixel(255, 84, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(255, 114, 0), new Pixel(254, 123, 0), new Pixel(253, 131, 0), new Pixel(251, 142, 0), new Pixel(249, 150, 0), new Pixel(247, 159, 0), new Pixel(244, 170, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(236, 196, 0), new Pixel(233, 203, 0), new Pixel(231, 210, 0), new Pixel(227, 217, 0), new Pixel(224, 223, 0), new Pixel(222, 227, 0), new Pixel(218, 232, 0), new Pixel(216, 236, 0), new Pixel(213, 239, 0), new Pixel(210, 242, 0), new Pixel(206, 245, 0), new Pixel(203, 247, 0), new Pixel(199, 249, 0), new Pixel(195, 251, 0), new Pixel(190, 252, 0), new Pixel(184, 254, 0), new Pixel(178, 255, 0), new Pixel(171, 255, 0), new Pixel(163, 255, 0), new Pixel(155, 255, 0), new Pixel(146, 255, 0), new Pixel(135, 255, 0), new Pixel(126, 255, 0), new Pixel(117, 255, 0), new Pixel(106, 255, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(80, 255, 0), new Pixel(73, 254, 4), new Pixel(66, 253, 18), new Pixel(57, 252, 39), new Pixel(50, 251, 60), new Pixel(43, 250, 83), new Pixel(35, 248, 112), new Pixel(28, 246, 136), new Pixel(22, 245, 161), new Pixel(16, 242, 189), new Pixel(11, 239, 210), new Pixel(6, 236, 229), new Pixel(1, 232, 247), new Pixel(0, 228, 255), new Pixel(0, 223, 255), new Pixel(0, 217, 255), new Pixel(0, 211, 255), new Pixel(0, 204, 255), new Pixel(0, 196, 255), new Pixel(0, 189, 255), new Pixel(0, 182, 255), new Pixel(0, 174, 255), new Pixel(0, 167, 255), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 123, 255), new Pixel(0, 116, 255), new Pixel(0, 108, 255), new Pixel(0, 101, 255), new Pixel(0, 94, 255), new Pixel(0, 86, 255), new Pixel(0, 80, 255), new Pixel(0, 73, 255), new Pixel(0, 66, 255), new Pixel(1, 60, 255), new Pixel(4, 54, 255), new Pixel(8, 47, 255), new Pixel(11, 41, 255), new Pixel(15, 36, 255), new Pixel(21, 29, 255), new Pixel(26, 24, 255), new Pixel(32, 19, 255), new Pixel(40, 13, 255), new Pixel(47, 9, 255), new Pixel(55, 5, 255), new Pixel(66, 1, 255), new Pixel(76, 0, 251), new Pixel(88, 0, 243), new Pixel(105, 0, 231), new Pixel(120, 0, 219), new Pixel(137, 0, 206), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(210, 0, 146), new Pixel(225, 0, 134), new Pixel(237, 0, 123), new Pixel(250, 0, 113), new Pixel(255, 0, 107), new Pixel(255, 0, 103), new Pixel(255, 0, 100), new Pixel(255, 0, 99), new Pixel(255, 0, 99), },{ new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 3, 0), new Pixel(255, 5, 0), new Pixel(255, 8, 0), new Pixel(255, 12, 0), new Pixel(255, 18, 0), new Pixel(255, 26, 0), new Pixel(255, 33, 0), new Pixel(255, 42, 0), new Pixel(255, 53, 0), new Pixel(255, 63, 0), new Pixel(255, 72, 0), new Pixel(255, 84, 0), new Pixel(255, 94, 0), new Pixel(255, 104, 0), new Pixel(255, 114, 0), new Pixel(254, 123, 0), new Pixel(253, 131, 0), new Pixel(251, 142, 0), new Pixel(249, 150, 0), new Pixel(247, 159, 0), new Pixel(244, 170, 0), new Pixel(242, 178, 0), new Pixel(239, 186, 0), new Pixel(236, 196, 0), new Pixel(233, 203, 0), new Pixel(231, 210, 0), new Pixel(227, 217, 0), new Pixel(224, 223, 0), new Pixel(222, 227, 0), new Pixel(218, 232, 0), new Pixel(216, 236, 0), new Pixel(213, 239, 0), new Pixel(210, 242, 0), new Pixel(206, 245, 0), new Pixel(203, 247, 0), new Pixel(199, 249, 0), new Pixel(195, 251, 0), new Pixel(190, 252, 0), new Pixel(184, 254, 0), new Pixel(178, 255, 0), new Pixel(171, 255, 0), new Pixel(163, 255, 0), new Pixel(155, 255, 0), new Pixel(146, 255, 0), new Pixel(135, 255, 0), new Pixel(126, 255, 0), new Pixel(117, 255, 0), new Pixel(106, 255, 0), new Pixel(98, 255, 0), new Pixel(89, 255, 0), new Pixel(80, 255, 0), new Pixel(73, 254, 4), new Pixel(66, 253, 18), new Pixel(57, 252, 39), new Pixel(50, 251, 60), new Pixel(43, 250, 83), new Pixel(35, 248, 112), new Pixel(28, 246, 136), new Pixel(22, 245, 161), new Pixel(16, 242, 189), new Pixel(11, 239, 210), new Pixel(6, 236, 229), new Pixel(1, 232, 247), new Pixel(0, 228, 255), new Pixel(0, 223, 255), new Pixel(0, 217, 255), new Pixel(0, 211, 255), new Pixel(0, 204, 255), new Pixel(0, 196, 255), new Pixel(0, 189, 255), new Pixel(0, 182, 255), new Pixel(0, 174, 255), new Pixel(0, 167, 255), new Pixel(0, 160, 255), new Pixel(0, 151, 255), new Pixel(0, 145, 255), new Pixel(0, 138, 255), new Pixel(0, 130, 255), new Pixel(0, 123, 255), new Pixel(0, 116, 255), new Pixel(0, 108, 255), new Pixel(0, 101, 255), new Pixel(0, 94, 255), new Pixel(0, 86, 255), new Pixel(0, 80, 255), new Pixel(0, 73, 255), new Pixel(0, 66, 255), new Pixel(1, 60, 255), new Pixel(4, 54, 255), new Pixel(8, 47, 255), new Pixel(11, 41, 255), new Pixel(15, 36, 255), new Pixel(21, 29, 255), new Pixel(26, 24, 255), new Pixel(32, 19, 255), new Pixel(40, 13, 255), new Pixel(47, 9, 255), new Pixel(55, 5, 255), new Pixel(66, 1, 255), new Pixel(76, 0, 251), new Pixel(88, 0, 243), new Pixel(105, 0, 231), new Pixel(120, 0, 219), new Pixel(137, 0, 206), new Pixel(157, 0, 190), new Pixel(174, 0, 176), new Pixel(191, 0, 162), new Pixel(210, 0, 146), new Pixel(225, 0, 134), new Pixel(237, 0, 123), new Pixel(250, 0, 113), new Pixel(255, 0, 107), new Pixel(255, 0, 103), new Pixel(255, 0, 100), new Pixel(255, 0, 99), new Pixel(255, 0, 99) } };

        public static IPixel[,] image = { { new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), }, { new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), }, { new Pixel(0, 0, 0), new Pixel(7, 6, 4), new Pixel(46, 32, 25), new Pixel(54, 36, 27), new Pixel(54, 34, 25), new Pixel(61, 40, 30), new Pixel(56, 38, 29), new Pixel(21, 16, 11), new Pixel(0, 0, 0), new Pixel(0, 0, 0), }, { new Pixel(16, 11, 8), new Pixel(103, 69, 54), new Pixel(143, 97, 80), new Pixel(139, 90, 73), new Pixel(125, 76, 63), new Pixel(124, 77, 62), new Pixel(131, 82, 66), new Pixel(110, 72, 56), new Pixel(75, 51, 38), new Pixel(25, 18, 14), }, { new Pixel(126, 81, 63), new Pixel(150, 94, 74), new Pixel(185, 126, 110), new Pixel(200, 142, 129), new Pixel(210, 159, 150), new Pixel(201, 148, 138), new Pixel(175, 120, 111), new Pixel(160, 111, 98), new Pixel(117, 77, 63), new Pixel(111, 78, 64), }, { new Pixel(142, 92, 75), new Pixel(196, 134, 120), new Pixel(223, 159, 147), new Pixel(235, 177, 172), new Pixel(234, 179, 176), new Pixel(230, 176, 173), new Pixel(220, 163, 160), new Pixel(193, 138, 132), new Pixel(135, 91, 81), new Pixel(126, 88, 75), }, { new Pixel(181, 130, 116), new Pixel(222, 161, 149), new Pixel(233, 173, 162), new Pixel(238, 181, 174), new Pixel(236, 184, 179), new Pixel(234, 183, 180), new Pixel(222, 166, 163), new Pixel(208, 151, 149), new Pixel(183, 136, 129), new Pixel(144, 105, 95), }, { new Pixel(196, 146, 132), new Pixel(224, 165, 154), new Pixel(236, 177, 169), new Pixel(240, 189, 184), new Pixel(236, 188, 185), new Pixel(235, 187, 184), new Pixel(226, 174, 171), new Pixel(214, 161, 157), new Pixel(185, 138, 131), new Pixel(146, 108, 101), }, { new Pixel(184, 134, 120), new Pixel(203, 153, 142), new Pixel(196, 144, 137), new Pixel(194, 143, 137), new Pixel(222, 172, 169), new Pixel(214, 164, 161), new Pixel(187, 137, 132), new Pixel(184, 135, 131), new Pixel(166, 120, 115), new Pixel(141, 102, 95), }, { new Pixel(190, 140, 129), new Pixel(194, 143, 133), new Pixel(144, 111, 109), new Pixel(193, 145, 142), new Pixel(225, 177, 171), new Pixel(201, 153, 149), new Pixel(189, 145, 144), new Pixel(140, 109, 108), new Pixel(160, 114, 109), new Pixel(133, 91, 85), }, { new Pixel(205, 149, 135), new Pixel(225, 172, 161), new Pixel(224, 172, 164), new Pixel(225, 174, 166), new Pixel(229, 172, 163), new Pixel(206, 152, 145), new Pixel(211, 160, 156), new Pixel(212, 163, 159), new Pixel(201, 152, 148), new Pixel(137, 94, 87), }, { new Pixel(212, 151, 138), new Pixel(236, 177, 170), new Pixel(235, 179, 174), new Pixel(224, 162, 153), new Pixel(230, 168, 159), new Pixel(206, 147, 139), new Pixel(200, 145, 137), new Pixel(228, 176, 172), new Pixel(214, 156, 153), new Pixel(147, 98, 89), }, { new Pixel(170, 120, 110), new Pixel(223, 166, 161), new Pixel(210, 152, 146), new Pixel(211, 149, 143), new Pixel(195, 137, 132), new Pixel(165, 103, 98), new Pixel(180, 121, 117), new Pixel(200, 146, 143), new Pixel(201, 147, 144), new Pixel(106, 69, 60), }, { new Pixel(109, 80, 74), new Pixel(200, 150, 142), new Pixel(195, 140, 136), new Pixel(196, 134, 132), new Pixel(198, 141, 140), new Pixel(188, 130, 129), new Pixel(158, 100, 98), new Pixel(173, 118, 116), new Pixel(191, 146, 140), new Pixel(72, 47, 43), }, { new Pixel(77, 56, 51), new Pixel(202, 155, 148), new Pixel(211, 159, 153), new Pixel(187, 132, 129), new Pixel(206, 154, 153), new Pixel(198, 143, 144), new Pixel(171, 109, 112), new Pixel(195, 141, 138), new Pixel(172, 127, 121), new Pixel(45, 29, 26), }, { new Pixel(33, 24, 21), new Pixel(179, 132, 123), new Pixel(213, 161, 151), new Pixel(219, 161, 154), new Pixel(219, 160, 158), new Pixel(209, 149, 148), new Pixel(196, 144, 140), new Pixel(186, 139, 134), new Pixel(120, 83, 78), new Pixel(5, 3, 3), }, { new Pixel(0, 0, 0), new Pixel(50, 35, 32), new Pixel(182, 135, 125), new Pixel(221, 162, 151), new Pixel(223, 163, 157), new Pixel(216, 155, 154), new Pixel(200, 143, 140), new Pixel(132, 93, 88), new Pixel(13, 9, 8), new Pixel(0, 0, 0), }, { new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(15, 11, 10), new Pixel(77, 56, 53), new Pixel(91, 65, 65), new Pixel(87, 61, 60), new Pixel(68, 47, 46), new Pixel(9, 6, 6), new Pixel(0, 0, 0), new Pixel(0, 0, 0), }, { new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), }, { new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), } };

        public static IPixel[] rainbow = {
                new Pixel(230, 0, 2), new Pixel(255, 106, 0),
                new Pixel(252, 154, 3), new Pixel(253, 220, 89),
                new Pixel(162, 207, 0), new Pixel(35, 169, 74),
                new Pixel(35, 169, 204), new Pixel(2, 82, 141),
                new Pixel(151, 14, 128), new Pixel(236, 19, 132) };
    

        static int score = 0;

        public static int[] cats = { 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 };

        public class Coord
        {
            public int Row;
            public int Col;
            public Pixel Colour;
        }

        public class Bat
        {
            public int Col;
            public int shootFrameDelay;
            public int framesTillShoot;

            public Bat()
            {
                shootFrameDelay = 10;
                Col = 5;
            }
        }

        public static Bat bat = new Bat();

        public class Laser
        {
            public Coord[] Bolt;

            public Laser(int pCol)
            {
                Bolt = new Coord[4];

                for (int i = 0; i < Bolt.Length; i++)
                {
                    Bolt[i] = new Coord();
                    Bolt[i].Col = pCol;
                    Bolt[i].Row = board.GetLength(0) - 3 + i;
                }

                Bolt[0].Colour = new Pixel(255, 255, 255);
                Bolt[1].Colour = new Pixel(200, 200, 225);
                Bolt[2].Colour = new Pixel(150, 150, 225);
                Bolt[3].Colour = new Pixel(75, 75, 255);
            }
        }

        public static List<Coord> Falling = new List<Coord>();

        public static List<Laser> Lasers = new List<Laser>();

        public class FrameTimer
        {
            public int frameDelay;
            public int timeTillThing;

            public bool DoThing()
            {
                timeTillThing--;
                if(timeTillThing <= 0)
                {
                    timeTillThing = frameDelay;
                    return true;
                }
                return false;
            }

            public void ModifyPeriod(int pInt)
            {
                frameDelay += pInt;
            }

            public FrameTimer(int pDelay)
            {
                frameDelay = pDelay;
                timeTillThing = pDelay;
            }
        }

        static FrameTimer spawnTimer = new FrameTimer(45);
        static FrameTimer dropTimer = new FrameTimer(30);

        #region
        
        // https://stackoverflow.com/questions/1335426/is-there-a-built-in-c-net-system-api-for-hsv-to-rgb

        public static void HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
        {
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((int)(R * 255.0));
            g = Clamp((int)(G * 255.0));
            b = Clamp((int)(B * 255.0));
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        static int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }

        #endregion

        static void UpdateBackground()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = background[i, j];
                }
            }
        }

        static void UpdateCats()
        {
            for (int i = 0; i < cats.Length; i++)
            {
                if (cats[i] != 0)
                {
                    int move = rng.Next(3);
                    switch (move)
                    {
                        case 1:
                            if (i > 0 && cats[i - 1] == 0)
                            {
                                cats[i - 1] = 1;
                                cats[i] = 0;
                            }
                            break;
                        case 2:
                            if (i != 9 && cats[i + 1] == 0)
                            {
                                cats[i + 1] = 1;
                                cats[i] = 0;
                            }
                            break;
                    }
                }
            }

            for (int i = 0; i < cats.Length; i++)
            {
                if (cats[i] == 1)
                {
                    board[board.GetLength(0) - 1, i] = new Pixel(0, 255, 0);
                }
                else
                {
                    board[board.GetLength(0) - 1, i] = new Pixel(255, 0, 0);
                }
            }
        }

        static void UpdateBat()
        {
            int batRow = board.GetLength(0) - 2;
            board[batRow, bat.Col] = background[batRow, bat.Col];

            bat.framesTillShoot = bat.framesTillShoot > 0 ? bat.framesTillShoot - 1 : 0;

            while (Console.KeyAvailable)
            {
                char ch = Console.ReadKey(true).KeyChar;

                switch (ch)
                {
                    case 'a':
                        bat.Col = bat.Col != 0 ? bat.Col - 1 : 0;
                        break;
                    case 'd':
                        bat.Col = bat.Col != 9 ? bat.Col + 1 : 9;
                        break;
                    case 's':
                        if (bat.framesTillShoot <= 0)
                        {
                            bat.framesTillShoot = bat.shootFrameDelay;
                            Laser laser = new Laser(bat.Col);
                            Lasers.Add(laser);
                        }

                        break;
                }
            }

            board[batRow, bat.Col] = new Pixel(255, 255, 255);
        }

        static void UpdateLasers()
        {
            foreach (Laser l in Lasers)
            {
                for (int i = 0; i < l.Bolt.Length; i++)
                {
                    l.Bolt[i].Row--;
                }
            }


            foreach (Laser l in Lasers)
            {
                foreach (Coord c in l.Bolt)
                {
                    if (c.Row < board.GetLength(0) && c.Row >= 0)
                    {
                        board[c.Row, c.Col] = c.Colour;
                    }
                }
            }

            CheckLaserCollisions();
        }

        private static void CheckLaserCollisions()
        {
            for (int laserIndex = Lasers.Count - 1; laserIndex >= 0; laserIndex--)
            {
                for (int fallerIndex = Falling.Count - 1; fallerIndex >= 0; fallerIndex--)
                {
                    if (Lasers[laserIndex].Bolt[0].Col == Falling[fallerIndex].Col &&
                        Lasers[laserIndex].Bolt[0].Row == Falling[fallerIndex].Row)
                    {
                        score++;
                        if (score % 2 == 0)
                        {
                            if (rng.Next(2) == 1)
                            {
                                spawnTimer.ModifyPeriod(-1);
                            }
                            else
                            {
                                dropTimer.ModifyPeriod(-1);
                            }
                        }
                        Lasers.RemoveAt(laserIndex);
                        Falling.RemoveAt(fallerIndex);
                        break;
                    }
                }
            }
        }

        static void UpdateFallingStuff()
        {
            if (dropTimer.DoThing())
            {
                for (int i = 0; i < Falling.Count; i++)
                {
                    Falling[i].Row++;
                }

                CheckLaserCollisions();
            }

            if (spawnTimer.DoThing())
            {
                Coord faller = new Coord();
                faller.Col = rng.Next(0, board.GetLength(1));
                faller.Row = 0;
                int i = rng.Next(9);
                while (i == faller.Col)
                {
                    i = rng.Next(9);
                }
                faller.Colour = (Pixel)rainbow[i];
                Falling.Add(faller);
            }

            for(int i = Falling.Count - 1; i >= 0; i--)
            {
                if(Falling[i].Row == board.GetLength(0))
                {
                    if(cats[Falling[i].Col] == 1)
                    {
                        cats[Falling[i].Col] = 0;
                    }

                    Falling.RemoveAt(i);
                }
            }

            foreach(Coord c in Falling)
            {
                board[c.Row, c.Col] = c.Colour;
            }
        }

        static FrameTimer titleScrollTimer = new FrameTimer(40000);

        static int titleOffset = 0;

        static void ScrollTitle()
        {
            if (titleScrollTimer.DoThing())
            {
                titleOffset += 1;

                if (titleOffset > title.GetLength(1) - 10)
                {
                    titleOffset = 0;
                }
            }

            for(int i = 0; i < 20; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    board[i, j] = title[i, j + titleOffset];
                }
            }
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < background.GetLength(0); i++)
            {
                for (int j = 0; j < background.GetLength(1); j++)
                {
                    background[i, j] = rainbow[j];
                    board[i, j] = rainbow[j];
                }
            }

            IDisplay display = new ArduinoDisplay();
            while (true)
            {
                state = State.Playing;
                if (state == State.Playing)
                {
                    UpdateBackground();
                    UpdateCats();
                    UpdateLasers();
                    UpdateBat();
                    UpdateFallingStuff();

                    Thread.Sleep(50);

                    int catsAlive = 0;

                    foreach(int c in cats)
                    {
                        if(c == 1) { catsAlive++; }
                    }

                    display.DisplayInts(catsAlive, score);
                    display.Draw(board);

                    bool allCatsDead = true;

                    foreach (int i in cats)
                    {
                        if (i == 1)
                        {
                            allCatsDead = false;
                            break;
                        }
                    }

                    if (allCatsDead)
                    {
                        state = State.GameOver;
                    }
                }
                else if(state == State.Title)
                {
                    ScrollTitle();
                    display.Draw(board);
                }
                else if(state == State.GameOver)
                {
                    display.Draw(image);
                }
            }
        }
    }
}