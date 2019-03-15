var AnnualPlanGenerator = function (config) {

    var currentMarketingYearModel = config.CurrentMarketingYearModel;

    var paragraphPoints = ['a)', 'b)', 'c)', 'd)', 'e)', 'f)'];

    var getLineStartXPosition = function (lines, currentLine) {
        var currentLineLength = currentLine.length;
        var longestLineLength = 0;
        for (let i = 0; i < lines.length; i++) {
            if (longestLineLength < lines[i].length) {
                longestLineLength = lines[i].length;
            }
        }

        if (longestLineLength === 0) {
            return longestLineLength;
        }

        var startPosition = ((longestLineLength - currentLineLength) / 2) * 6;
        return startPosition;
    }

    function drawString(text, fontSize, width, height, translateX, translateY, bold) {
        var ctx, canvas = document.createElement('canvas');
        canvas.width = width;
        canvas.height = height;
        ctx = canvas.getContext('2d');
        ctx.save();
        var font = fontSize + "pt Times New Roman";;
        if (bold) {
            font = "bold " + font;
        }
        ctx.font = font;
        ctx.fillStyle = '#000000';
        ctx.translate(translateX, translateY);
        ctx.rotate(-0.5 * Math.PI);
        var lines = text.split("\n");
        for (let i = 0; i < lines.length; i++) {
            var startPositionX = getLineStartXPosition(lines, lines[i]);
            ctx.fillText(lines[i], startPositionX, i * (fontSize + 5));
        }
        ctx.restore();
        return canvas.toDataURL();
    }

    var getNumbersRow = function (columnsNumber) {
        var numbersRow = [];
        for (var i = 1; i <= columnsNumber; i++) {
            numbersRow.push({ text: i, fontSize: 8, alignment: 'center', margin: [0, -2, 0, -2] });
        }
        return numbersRow;
    }

    var getDottedRows = function (array, rowsNumber) {
        var dottedLine = '....................................................................................................................................................................................................';

        for (var i = 1; i <= rowsNumber; i++) {
            array.push({ text: dottedLine, fontSize: 11, margin: [0, 10, 0, 0] });
        }
    }

    var getApprovalsSectionSignRows = function(array) {
        array.push({ text: '.............................................................', alignment: 'right', fontSize: 11, margin: [0, 30, 0, 0] });
        array.push({ text: '(data, podpis)', alignment: 'right', fontSize: 8, margin: [0, 0, 60, 0] });
    }

    var getPlanHeader = function () {
        var header = [];

        header.push({ text: 'ROCZNY  PLAN  ŁOWIECKI', alignment: 'center', fontSize: 12, bold: true });
        header.push({ text: 'na rok gospodarczy ' + currentMarketingYearModel.MarketingYear, alignment: 'center', fontSize: 10, bold: true, margin: [0, 10, 0, 50] });

        return header;
    }

    var getHuntClubInformation = function () {
        var huntClubInformation = [];

        huntClubInformation.push({
            text: [
                { text: '1. Obwód łowiecki nr ', fontSize: 10 },
                { text: ' 20 ', fontSize: 10, bold: true },
                { text: ' powierzchnia ', fontSize: 10 },
                { text: ' 4777 ', fontSize: 10, bold: true},
                { text: ' ha, w tym powierzchnia gruntów leśnych ', fontSize: 10 },
                { text: ' 4394 ', fontSize: 10, bold: true},
                { text: ' ha', fontSize: 10 }
            ], margin: [0, 20, 0, 0]
        });

        huntClubInformation.push({
            text: [
                { text: 'powierzchnia po wyłączeniach, o których mowa w art. 26 ustawy z 13.X.1995r.Prawo Łowieckie ', fontSize: 10 },
                { text: ' 4502 ', fontSize: 10, bold: true },
                { text: ' ha.', fontSize: 10 }
            ], margin: [0, 10, 0, 0]
        });

        huntClubInformation.push({
            text: [
                { text: '2. Województwo ', fontSize: 10 },
                { text: 'wielkopolskie, ', fontSize: 10, bold: true},
                { text: 'Powiat ', fontSize: 10},
                { text: 'złotowski.', fontSize: 10, bold: true }
            ], margin: [0, 15, 0, 0]
        });

        huntClubInformation.push({
            text: [
                { text: '3. Nadleśnictwo (nazwa i adres siedziby) ', fontSize: 10 },
                { text: 'Płytnica z siedzibą w Nowej Szwecji, Nowa Szwecja 6, 78-600 Wałcz.', fontSize: 10, bold: true }
            ], margin: [0, 15, 0, 0]
        });

        huntClubInformation.push({
            text: [
                { text: '4. Regionalna Dyrekcja Lasów Państwowych (nazwa i adres siedziby) ', fontSize: 10 },
                { text: 'Piła, 64-920 Piła-Kalina.', fontSize: 10, bold: true}
            ], margin: [0, 15, 0, 0]
        });

        huntClubInformation.push({
            text: [
                { text: '5. Zarząd Okręgowy PZŁ (nazwa i adres siedziby) ', fontSize: 10},
                { text: 'Piła, Al. Powstańców Wielkopolskich 190, 64-920 Piła.', fontSize: 10, bold: true }
            ], margin: [0, 15, 0, 0]
        });

        huntClubInformation.push({
            text: [
                { text: '6. Dzierżawca lub zarządca (nazwa i adres siedziby) ', fontSize: 10},
                { text: 'Koło Łowieckie nr 39 Literatów Polskich „Pióro” ul. Rostworowskiego 6/2,', fontSize: 10, bold: true }
            ], margin: [0, 15, 0, 0] 
        });

        huntClubInformation.push({
            text: [
                { text: '01-496 Warszawa.', fontSize: 10, bold: true }
            ], margin: [0, 10, 0, 0]
        });

        huntClubInformation.push({
            text: [
                { text: '7. Data sporządzenia rocznego planu łowieckiego: ', fontSize: 10 },
                { text: '03/03/2019', fontSize: 10, bold: true }
            ], margin: [0, 15, 0, 0]
        });
        
        huntClubInformation.push({ text: '8. Osoba uprawniona do reprezentowania dzierżawcy albo zarządcy obwodu łowieckiego (imię, nazwisko) ', fontSize: 10, margin: [0, 15, 0, 10]});
        huntClubInformation.push({ text: '....................................................................................................................................................................................................', fontSize: 11 });
        huntClubInformation.push({ text: '9. ................................................................................................................................................................................................', fontSize: 11, margin: [0, 50, 0, 0] });
        huntClubInformation.push({ text: '(podpis osoby uprawnionej do reprezentacji dzierżawcy albo zarządcy obwodu łowieckiego)', alignment:'center', fontSize: 8, margin: [0, 0, 0, 30] });
        huntClubInformation.push({ text: 'Plan zatwierdził', alignment:'right', fontSize: 10, margin: [0, 20, 130, 30] });
        huntClubInformation.push({ text: '.................................................................................', alignment: 'right', fontSize: 11, margin: [0, 10, 50, 0] });
        huntClubInformation.push({ text: '(data, podpis)', alignment: 'right', fontSize: 8, margin: [0, 0, 140, 0] });

        return huntClubInformation;
    }
    
    var getEconomyTableHeaders = function() {
        var headers = [];

        headers.push({ text: 'Wyszczególnienie', style: 'header', margin: [0, 40, 0, 0] });
        headers.push({ text: 'jedn. miary', style: 'header', margin: [0, 35, 0, 0] });
        headers.push({ text: 'Stan planowany do realizacji w łowieckim roku gospodarczym poprzedzającym łowiecki rok gospodarczy, na który sporządzono rpł.', alignment: 'center', fontSize: 8, bold: true, margin:[0, 5, 0, 0] });
        headers.push({ text: 'Stan wynikający z realizacji rpł. obowiązującego łowieckiego roku gospodarczego poprzedzającego łowiecki rok gospodarczy, na który sporządzono rpł.', alignment: 'center', fontSize: 8, bold: true });
        headers.push({ text: 'Stan na dzień 10 marca roku, w którym jest sporządzany roczny plan łowiecki', alignment: 'center', fontSize: 8, bold: true, margin: [0, 20, 0, 0] });
        headers.push({ text: 'Stan planowany do realizacji w łowieckim roku gospodarczym, na który jest sporządzany roczny plan łowiecki', alignment: 'center', fontSize: 8, bold: true, margin: [0, 15, 0, 0] });

        return headers;
    };

    var getEconomyTableBody = function (previousAnnualPlanModel, currentAnnualPlanModel) {
        var body = []; 

        body.push([
            { text: '1. Liczba osób zatrudnionych na podstawie umowy o pracę', style: 'paragraph' },
            { text: 'osoby/etaty', margin: [0, 3, 0, 0], style: 'cell' },
            { text: previousAnnualPlanModel.EmployeePlanModel.FullTimeEmployees, style: 'cell', margin:[0, 10, 0, 0]},
            { text: '', style: 'cell'},
            { text: '', style: 'cell'},
            { text: currentAnnualPlanModel.EmployeePlanModel.FullTimeEmployees, style: 'cell', margin: [0, 10, 0, 0]}
        ]);

        body.push([
            { text: '2. Liczba osób zatrudnionych na innej podstawie niż umowa o pracę lub wskazanych do wykonywania zadań z zakresu gospodarki łowieckiej', style: 'paragraph' },
            { text: 'osoby', margin: [0, 10, 0, 0], style: 'cell' },
            { text: previousAnnualPlanModel.EmployeePlanModel.PartTimeEmployees, style: 'cell', margin: [0, 10, 0, 0] },
            { text: '', style: 'cell' },
            { text: '', style: 'cell' },
            { text: currentAnnualPlanModel.EmployeePlanModel.PartTimeEmployees, style: 'cell', margin: [0, 10, 0, 0] }
        ]);

        body.push([
            { text: '3. Urządzenia związane z prowadzeniem gospodarki łowieckiej', style: 'paragraph' },
            { text: 'X', style: 'cell', margin: [0, 5, 0, 0] },
            { text: 'X', style: 'cell', margin: [0, 5, 0, 0] },
            { text: 'X', style: 'cell', margin: [0, 5, 0, 0] },
            { text: 'X', style: 'cell', margin: [0, 5, 0, 0] },
            { text: 'X', style: 'cell', margin: [0, 5, 0, 0] }
        ]);

        body.push([
            { text: 'a) paśniki', style: 'point', margin:[5, -2, 0, -1] },
            { text: 'szt.', style: 'cell', margin: [0, -2, 0, -1] },
            { text: previousAnnualPlanModel.HuntEquipmentPlanModel.Pastures, style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: currentAnnualPlanModel.HuntEquipmentPlanModel.Pastures, style: 'cell', margin: [0, -2, 0, -1] }
        ]);

        body.push([
            { text: 'b) lizawki', style: 'point', margin: [5, -2, 0, -1] },
            { text: 'szt.', style: 'cell', margin: [0, -2, 0, -1] },
            { text: previousAnnualPlanModel.HuntEquipmentPlanModel.DeerLickers, style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: currentAnnualPlanModel.HuntEquipmentPlanModel.DeerLickers, style: 'cell', margin: [0, -2, 0, -1] }
        ]);

        body.push([
            { text: 'c) ambony', style: 'point', margin: [5, -2, 0, -1] },
            { text: 'szt.', style: 'cell', margin: [0, -2, 0, -1] },
            { text: previousAnnualPlanModel.HuntEquipmentPlanModel.Pulpits, style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: currentAnnualPlanModel.HuntEquipmentPlanModel.Pulpits, style: 'cell', margin: [0, -2, 0, -1] }
        ]);

        body.push([
            { text: 'd) woliery', style: 'point', margin: [5, -2, 0, -1] },
            { text: 'szt.', style: 'cell', margin: [0, -2, 0, -1] },
            { text: previousAnnualPlanModel.HuntEquipmentPlanModel.Aviaries, style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: currentAnnualPlanModel.HuntEquipmentPlanModel.Aviaries, style: 'cell', margin: [0, -2, 0, -1] }
        ]);

        body.push([
            { text: 'e) zagrody', style: 'point', margin: [5, -2, 0, -1] },
            { text: 'szt.', style: 'cell', margin: [0, -2, 0, -1] },
            { text: previousAnnualPlanModel.HuntEquipmentPlanModel.Farms, style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: currentAnnualPlanModel.HuntEquipmentPlanModel.Farms, style: 'cell', margin: [0, -2, 0, -1] }
        ]);

        body.push([
            { text: 'f) inne', style: 'point', margin: [5, -2, 0, -1] },
            { text: 'szt.', style: 'cell', margin: [0, -2, 0, -1] },
            { text: previousAnnualPlanModel.HuntEquipmentPlanModel.WateringPlaces, style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: currentAnnualPlanModel.HuntEquipmentPlanModel.WateringPlaces, style: 'cell', margin: [0, -2, 0, -1] }
        ]);
        
        body.push([
            { text: '4. Liczba i łączna długość pasów zaporowych', rowSpan: 2, style: 'paragraph', margin: [0, -2, 0, -1] },
            { text: 'szt.', style: 'cell', margin: [0, -2, 0, -1] },
            { text: '0', style: 'cell', margin: [0, -2, 0, -1] },
            { text: '0', style: 'cell', margin: [0, -2, 0, -1] },
            { text: '0', style: 'cell', margin: [0, -2, 0, -1] },
            { text: '0', style: 'cell', margin: [0, -2, 0, -1] }
        ]);

        body.push([
            { text: '', margin: [0, -2, 0, -1] },
            { text: 'km', style: 'cell', margin: [0, -2, 0, -1] },
            { text: previousAnnualPlanModel.BarrierPlanModel.Length, style: 'cell', margin: [0, -2, 0, -1] },
            { text: '0', style: 'cell', margin: [0, -2, 0, -1] },
            { text: '0', style: 'cell', margin: [0, -2, 0, -1] },
            { text: currentAnnualPlanModel.BarrierPlanModel.Length, style: 'cell', margin: [0, -2, 0, -1] }
        ]);

        body.push([
            { text: '5. Powierzchnia obszarów obsianych lub obsadzonych roślinami stanowiącymi żer dla zwierzyny na pniu', style: 'paragraph' },
            { text: 'ha', margin: [0, 5, 0, 0], style: 'cell' },
            { text: previousAnnualPlanModel.TrunkFoodPlanModel.Hectare, style: 'cell', margin: [0, 5, 0, 0] },
            { text: '', style: 'cell' },
            { text: '', style: 'cell' },
            { text: currentAnnualPlanModel.TrunkFoodPlanModel.Hectare, style: 'cell', margin: [0, 5, 0, 0] }
        ]);

        body.push([
            { text: '6. Powierzchnia zagospodarowanych przez dzierżawcę albo zarządcę obwodu łowieckiego łąk śródleśnych i przyleśnych', style: 'paragraph' },
            { text: 'ha', margin: [0, 10, 0, 0], style: 'cell' },
            { text: previousAnnualPlanModel.FieldPlanModel.Hectare, style: 'cell', margin: [0, 10, 0, 0] },
            { text: '', style: 'cell' },
            { text: '', style: 'cell' },
            { text: currentAnnualPlanModel.FieldPlanModel.Hectare, style: 'cell', margin: [0, 10, 0, 0] }
        ]);

        body.push([
            { text: '7. Masa i rodzaj karmy', style: 'paragraph' },
            { text: 'X', style: 'cell' },
            { text: 'X', style: 'cell' },
            { text: 'X', style: 'cell' },
            { text: 'X', style: 'cell' },
            { text: 'X', style: 'cell' }
        ]);

        body.push([
            { text: 'a) objętościowa sucha', style: 'point', margin: [5, -2, 0, -1] },
            { text: 'tona', style: 'cell', margin: [0, -2, 0, -1] },
            { text: previousAnnualPlanModel.FodderPlanModel.Dry.Plan, style: 'cell', margin: [0, -2, 0, -1] },
            { text: previousAnnualPlanModel.FodderPlanModel.Dry.Execution, style: 'cell', margin: [0, -2, 0, -1]  },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: currentAnnualPlanModel.FodderPlanModel.Dry.Plan, style: 'cell', margin: [0, -2, 0, -1] }
        ]);

        body.push([
            { text: 'b) objętościowa soczysta', style: 'point', margin: [5, -2, 0, -1] },
            { text: 'tona', style: 'cell', margin: [0, -2, 0, -1]  },
            { text: previousAnnualPlanModel.FodderPlanModel.Juicy.Plan, style: 'cell', margin: [0, -2, 0, -1] },
            { text: previousAnnualPlanModel.FodderPlanModel.Juicy.Execution, style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: currentAnnualPlanModel.FodderPlanModel.Juicy.Plan, style: 'cell', margin: [0, -2, 0, -1] }
        ]);

        body.push([
            { text: 'c) treściwa', style: 'point', margin: [5, -2, 0, -1]  },
            { text: 'tona', style: 'cell', margin: [0, -2, 0, -1]  },
            { text: previousAnnualPlanModel.FodderPlanModel.Pithy.Plan, style: 'cell', margin: [0, -2, 0, -1]  },
            { text: previousAnnualPlanModel.FodderPlanModel.Pithy.Execution, style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1]  },
            { text: currentAnnualPlanModel.FodderPlanModel.Pithy.Plan, style: 'cell', margin: [0, -2, 0, -1]  }
        ]);

        body.push([
            { text: 'd) sól', style: 'point', margin: [5, -2, 0, -1] },
            { text: 'tona', style: 'cell', margin: [0, -2, 0, -1]  },
            { text: previousAnnualPlanModel.FodderPlanModel.Salt.Plan, style: 'cell', margin: [0, -2, 0, -1] },
            { text: previousAnnualPlanModel.FodderPlanModel.Salt.Execution, style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: currentAnnualPlanModel.FodderPlanModel.Salt.Plan, style: 'cell', margin: [0, -2, 0, -1] }
        ]);

        body.push([
            { text: '8. Wielkość szkód wyrządzonych w uprawach i płodach rolnych przez dziki, łosie, jelenie, daniele i sarny', style: 'paragraph', margin: [-3, 0, -5, 0] },
            { text: 'ha', margin: [0, 5, 0, 0], style: 'cell' },
            { text: previousAnnualPlanModel.DamagedFieldPlanModel.Hectare, style: 'cell', margin: [0, 5, 0, 0] },
            { text: '', style: 'cell' },
            { text: '', style: 'cell' },
            { text: currentAnnualPlanModel.DamagedFieldPlanModel.Hectare, style: 'cell', margin: [0, 5, 0, 0] }
        ]);

        body.push([
            { text: '- powierzchnia zredukowana', style: 'point', margin: [5, -2, 0, -1] },
            { text: 'tyś. zł', style: 'cell', margin: [0, -2, 0, -1] },
            { text: previousAnnualPlanModel.DamagedFieldPlanModel.Hectare, style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1]  },
            { text: '', style: 'cell', margin: [0, -2, 0, -1]  },
            { text: currentAnnualPlanModel.DamagedFieldPlanModel.Hectare, style: 'cell', margin: [0, -2, 0, -1] }
        ]);

        body.push([
            { text: '- kwota wypłaconych odszkodowań łowieckich', style: 'point', margin: [5, -2, 0, -1] },
            { text: 'tyś. zł', style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] },
            { text: '', style: 'cell', margin: [0, -2, 0, -1] }
        ]);

        body.push([
            { text: '9. Koszty poniesione na prowadzenie gospodarki łowieckiej', style: 'paragraph' },
            { text: 'tyś. zł', margin: [0, 5, 0, 0], style: 'cell' },
            { text: previousAnnualPlanModel.CostPlanModel.Cost, style: 'cell', margin: [0, 5, 0, 0] },
            { text: '', style: 'cell' },
            { text: '', style: 'cell' },
            { text: currentAnnualPlanModel.CostPlanModel.Cost, style: 'cell', margin: [0, 5, 0, 0] }
        ]);

        return body;
    }
    
    var getRevenueTableBody = function (previousAnnualPlanModel, currentAnnualPlanModel) {
        var body = [];

        body.push([
            { text: '1. Przychody ze sprzedaży tusz zwierzyny płowej w obwodzie łowieckim', style: 'paragraph' },
            { text: 'tyś. zł', style: 'cell', margin: [0, 5, 0, 0] },
            { text: previousAnnualPlanModel.CostPlanModel.Revenue, style: 'cell', margin: [0, 5, 0, 0]  },
            { text: '', style: 'cell' },
            { text: '', style: 'cell' },
            { text: currentAnnualPlanModel.CostPlanModel.Revenue, style: 'cell', margin: [0, 5, 0, 0]  }
        ]);

        return body;
    }

    var getBigGameTableHeaders = function() {
        var headers = [];

        headers.push([
            { text: 'Gatunki zwierząt\nłownych', rowSpan: 3, alignment: 'center', fontSize: 9, bold: true, margin: [0, 70, 0, -70] },
            { image: drawString('Liczba zw. łow. planow.\ndo poz.w łow.roku gosp.\npoprz.rok gosp.na który\nsporządzono rpł.', 12, 110, 190, 15, 175, true), colSpan: 2, fit: [55, 130] },
            { text: '' },
            { image: drawString('Liczba zw. łow.\npozyskanych w łow. roku\ngosp. poprz. łow. rok.\ngosp. na który\nsporządzony jest rpl.', 12, 110, 190, 30, 180, true), colSpan: 3, fit: [55, 130] },
            { text: '' },
            { text: '' },
            { image: drawString('Liczba ubytków zw.\ngrubej powstałych w łow.\nroku gosp. poprz. łow.\nrok. gosp. na który\nsporządzono rpl. w szt.', 12, 110, 190, 15, 180, true), colSpan: 2, fit: [55, 130] },
            { text: '' },
            { image: drawString('Liczba zw. łow. zasiedl. w\nłow. r. gosp. poprz. łow.\nr. gosp. na który\nsporządzono rpl.', 12, 110, 190, 15, 180, true), fit: [55, 130], margin:[-3, 0, 0, 0] },
            { image: drawString('Szacowana liczebność\nzwierząt łownych\nna dzień 10 marca ' + config.CurrentMarketingYearModel.StartYear + ' r.', 12, 110, 190, 15, 180, true), fit: [55, 130] },
            { image: drawString('Planowana do zasiedleń\nliczba zw. łownych', 12, 110, 180, 15, 170, true), fit: [55, 130], margin: [-3, 0, 0, 0] },
            { image: drawString('Planowana liczebność\nzwierzyny grubej w dniu\npoprzedzającym dzień\nrozpoczęcia okresu\npolowań', 12, 110, 190, 15, 175, true), fit: [55, 130], margin:[-5, 0, 0, 0] },
            { image: drawString('Optymalna liczba\nzwierząt zaplanowanych\ndo pozyskania w łow.\nroku gosp. na który\nsporządzono rpl.', 12, 180, 190, 15, 180, true), colSpan: 2, fit: [90, 130] },
            { text: '' },
            { image: drawString('Minimalna i maksymalna\nliczba zwierząt\nzaplanowana do\npozyskania w łow. roku\ngospodarczym, na który\nsporządzono rpl.', 12, 180, 190, 30, 180, true), colSpan: 4, fit: [90, 130] },
            { text: '' },
            { text: '' },
            { text: '' }
        ]);

        headers.push([
            { text: '' },
            { image: drawString('odstrzał szt.', 16, 50, 120, 23, 110, false), fit: [20, 50], rowSpan: 2, margin: [0, 0, 0, -15] },
            { image: drawString('odłów szt.', 16, 50, 120, 23, 105, false), fit: [20, 50], rowSpan: 2, margin: [0, 0, 0, -15] },
            { image: drawString('ogółem szt.', 16, 50, 120, 23, 110, false), fit: [20, 50], rowSpan: 2, margin: [0, 0, 0, -15] },
            { image: drawString('odstrzał szt.', 16, 50, 120, 23, 110, false), fit: [20, 50], rowSpan: 2, margin: [0, 0, 0, -15] },
            { image: drawString('odłów szt.', 16, 50, 120, 23, 105, false), fit: [20, 50], rowSpan: 2, margin: [0, 0, 0, -15] },
            { image: drawString('ogółem', 16, 50, 120, 25, 90, false), fit: [20, 50], rowSpan: 2, margin: [0, 0, 0, -15] },
            { image: drawString('w tym odstrzał\nsanitarny', 16, 50, 140, 23, 130, false), fit: [20, 50], rowSpan: 2, margin: [0, 0, 0, -15] },
            { text: 'szt.', rowSpan: 2, alignment: 'center', fontSize: 8, margin: [0, 15, 0, -15] },
            { text: 'szt.', rowSpan: 2, alignment: 'center', fontSize: 8, margin: [0, 15, 0, -15] },
            { text: 'szt.', rowSpan: 2, alignment: 'center', fontSize: 8, margin: [0, 15, 0, -15] },
            { text: 'szt.', rowSpan: 2, alignment: 'center', fontSize: 8, margin: [0, 15, 0, -15] },
            { image: drawString('odstrzał szt.', 16, 50, 120, 25, 110, false), fit: [20, 50], rowSpan: 2, margin: [0, 0, 0, -15] },
            { image: drawString('odłów szt.', 16, 50, 120, 25, 105, false), fit: [20, 50], rowSpan: 2, margin: [0, 0, 0, -15] },
            { text: 'odstrzał szt.', colSpan: 2, alignment: 'center', fontSize: 8, margin: [-3, 0, -4, 0]  },
            { text: '' },
            { text: 'odłów szt.', colSpan: 2, alignment: 'center', fontSize: 8, margin: [-3, 0, -3, 0] },
            { text: '' }
        ]);

        headers.push([
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { image: drawString('min', 16, 20, 60, 16, 55), fit: [15, 25] },
            { image: drawString('max', 16, 20, 60, 16, 55), fit: [15, 25] },
            { image: drawString('min', 16, 20, 60, 16, 55), fit: [15, 25] },
            { image: drawString('max', 16, 20, 60, 16, 55), fit: [15, 25] }
        ]);
        
        headers.push(getNumbersRow(18));

        return headers;
    }

    var getBigGameTableBody = function (bigGamePlanModel) {
        var bigGameTableBody = [];

        var cellStyle = currentMarketingYearModel.Id >= 3 ? 'cell' : 'smallCell';
        var pointStyle = currentMarketingYearModel.Id >= 3 ? 'point' : 'smallPoint';
        
        $.each(bigGamePlanModel.AnnualPlanKindGameModels, function(key, value) {
            
            bigGameTableBody.push([
                { text: value.KindName + ' razem', bold: true, style: pointStyle, margin: [0, -2, 0, -1] },
                { text: value.PreviousHuntPlanCulls, style: cellStyle, margin: [0, -2, 0, -1] },
                { text: value.PreviousHuntPlanCatches, style: cellStyle, margin: [0, -2, 0, -1] },
                { text: value.PreviousHuntPlanExecutionTotal, style: cellStyle, margin: [0, -2, 0, -1]  },
                { text: value.PreviousHuntPlanExecutionCulls, style: cellStyle, margin: [0, -2, 0, -1] },
                { text: value.PreviousHuntPlanExecutionCatches, style: cellStyle, margin: [0, -2, 0, -1]  },
                { text: value.PreviousHuntPlanExecutionLosses, style: cellStyle, margin: [0, -2, 0, -1] },
                { text: value.PreviousHuntPlanExecutionSanitaryLosses, style: cellStyle, margin: [0, -2, 0, -1] },
                { text: '', margin: [0, -2, 0, -1] },
                { text: value.GameCountBefore10March, style: cellStyle, margin: [0, -2, 0, -1] },
                { text: '', margin: [0, -2, 0, -1] },
                { text: '', margin: [0, -2, 0, -1] },
                { text: value.CurrentHuntPlanCulls, style: cellStyle, margin: [0, -2, 0, -1] },
                { text: value.CurrentHuntPlanCatches, style: cellStyle, margin: [0, -2, 0, -1] } ,
                { text: value.CurrentHuntPlanCullsMin, style: cellStyle, margin: [0, -2, 0, -1] },
                { text: value.CurrentHuntPlanCullsMax, style: cellStyle, margin: [0, -2, 0, -1]  },
                { text: value.CurrentHuntPlanCatchesMin, style: cellStyle, margin: [0, -2, 0, -1]  },
                { text: value.CurrentHuntPlanCatchesMax, style: cellStyle, margin: [0, -2, 0, -1]  }
            ]);

            $.each(value.AnnualPlanSubKindGameModels, function (index, value) {

                var subKindName = (paragraphPoints[value.SubKind - 1] + ' ' + value.SubKindName).toLowerCase();
                if (index === 0) subKindName += ' razem';
                bigGameTableBody.push([
                    { text: subKindName, style: pointStyle, margin: [0, -2, 0, -1] },
                    { text: value.PreviousHuntPlanCulls, style: cellStyle, margin: [0, -2, 0, -1] },
                    { text: value.PreviousHuntPlanCatches, style: cellStyle, margin: [0, -2, 0, -1] },
                    { text: value.PreviousHuntPlanExecutionTotal, style: cellStyle, margin: [0, -2, 0, -1] },
                    { text: value.PreviousHuntPlanExecutionCulls, style: cellStyle, margin: [0, -2, 0, -1] },
                    { text: value.PreviousHuntPlanExecutionCatches, style: cellStyle, margin: [0, -2, 0, -1] },
                    { text: value.PreviousHuntPlanExecutionLosses, style: cellStyle, margin: [0, -2, 0, -1] },
                    { text: value.PreviousHuntPlanExecutionSanitaryLosses, style: cellStyle, margin: [0, -2, 0, -1]  },
                    { text: '', margin: [0, -2, 0, -1] },
                    { text: value.GameCountBefore10March, style: cellStyle, margin: [0, -2, 0, -1] },
                    { text: '', margin: [0, -2, 0, -1] },
                    { text: '', margin: [0, -2, 0, -1] },
                    { text: value.CurrentHuntPlanCulls, style: cellStyle, margin: [0, -2, 0, -1]  },
                    { text: value.CurrentHuntPlanCatches, style: cellStyle, margin: [0, -2, 0, -1] },
                    { text: value.CurrentHuntPlanCullsMin, style: cellStyle, margin: [0, -2, 0, -1]  },
                    { text: value.CurrentHuntPlanCullsMax, style: cellStyle, margin: [0, -2, 0, -1] },
                    { text: value.CurrentHuntPlanCatchesMin, style: cellStyle, margin: [0, -2, 0, -1] },
                    { text: value.CurrentHuntPlanCatchesMax, style: cellStyle, margin: [0, -2, 0, -1]  }
                ]);

                $.each(value.AnnualPlanClassGameModels, function (index, value) {

                    bigGameTableBody.push([
                        { text: '-' + value.ClassName, style: cellStyle, margin: [0, -2, 0, -1]  },
                        { text: value.PreviousHuntPlanCulls, style: cellStyle, margin: [0, -2, 0, -1] },
                        { text: value.PreviousHuntPlanCatches, style: cellStyle, margin: [0, -2, 0, -1] },
                        { text: value.PreviousHuntPlanExecutionTotal, style: cellStyle, margin: [0, -2, 0, -1] },
                        { text: value.PreviousHuntPlanExecutionCulls, style: cellStyle, margin: [0, -2, 0, -1]  },
                        { text: value.PreviousHuntPlanExecutionCatches, style: cellStyle, margin: [0, -2, 0, -1] },
                        { text: value.PreviousHuntPlanExecutionLosses, style: cellStyle, margin: [0, -2, 0, -1]  },
                        { text: value.PreviousHuntPlanExecutionSanitaryLosses, style: cellStyle, margin: [0, -2, 0, -1]  },
                        { text: '', margin: [0, -2, 0, -1] },
                        { text: value.GameCountBefore10March, style: cellStyle, margin: [0, -2, 0, -1] },
                        { text: '', margin: [0, -2, 0, -1] },
                        { text: '', margin: [0, -2, 0, -1] },
                        { text: value.CurrentHuntPlanCulls, style: cellStyle, margin: [0, -2, 0, -1] },
                        { text: value.CurrentHuntPlanCatches, style: cellStyle, margin: [0, -2, 0, -1] },
                        { text: value.CurrentHuntPlanCullsMin, style: cellStyle, margin: [0, -2, 0, -1] },
                        { text: value.CurrentHuntPlanCullsMax, style: cellStyle, margin: [0, -2, 0, -1] },
                        { text: value.CurrentHuntPlanCatchesMin, style: cellStyle, margin: [0, -2, 0, -1]  },
                        { text: value.CurrentHuntPlanCatchesMax, style: cellStyle, margin: [0, -2, 0, -1] }
                    ]);
                });
            });
        });

        return bigGameTableBody;
    }

    var getSmallGameTableHeaders = function() {
        var headers = [];

        headers.push([
            { text: 'Gatunki zwierząt\nłownych', rowSpan: 3, alignment: 'center', fontSize: 9, bold: true, margin: [0, 50, 0, -50] },
            { image: drawString('Liczba zw. łow.\nplanow. do poz. w\nłow. roku gosp.\npoprz. rok gosp. na\nktóry sporządzono\nrpl.', 13, 110, 160, 15, 150, true), colSpan: 2, fit:[55, 100] },
            { text: '' },
            { image: drawString('Liczba zw. łow.\npozyskanych w łow.\nroku gosp. poprz.\nłow. rok. gosp. na\nktóry sporządzony\njest rpl.', 13, 110, 160, 15, 150, true), colSpan: 2, fit: [55, 100] },
            { text: '' },
            { image: drawString('Liczba zw. łow.\nzasiedl. w łow. r.\ngosp. poprz. łow. r.\ngosp. na który\nsporządzono rpl.', 13, 110, 160, 15, 150, true), fit: [55, 100] },
            { image: drawString('Szacowana\nliczebność zwierząt\nłownych\nna dzień 10 marca.', 13, 110, 160, 15, 150, true), fit: [55, 100] },
            { image: drawString('Planow. do zasiedleń\nliczba zw. łownych', 13, 110, 160, 15, 155, true), fit: [55, 100] },
            { image: drawString('Optymalna liczba\nzwierząt\nzaplanowanych do\npozyskania w łow.\nroku gosp. na który\nsporządzono rpl.', 13, 110, 160, 15, 150, true), colSpan: 2, fit: [55, 100] },
            { text: '' },
            { image: drawString('Minimalna i\nmaksymalna liczba\nzwierząt\nzaplanowana do\npozyskania w łow.\nroku gospodarczym,\nna który\nsporządzono rpl.', 13, 160, 160, 15, 155, true), colSpan: 4, fit: [75, 100] },
            { text: '' },
            { text: '' },
            { text: '' }
        ]);

        headers.push([
            { text: '' },
            { image: drawString('odstrzał\nszt.', 16, 50, 90, 23, 80, false), fit: [25, 35], rowSpan: 2, margin: [0, 0, 0, -15] },
            { image: drawString('odłów\nszt.', 16, 50, 90, 23, 72, false), fit: [25, 35], rowSpan: 2, margin: [0, 0, 0, -15] },
            { image: drawString('odstrzał\nszt.', 16, 50, 90, 23, 80, false), fit: [25, 35], rowSpan: 2, margin: [0, 0, 0, -15] },
            { image: drawString('odłów\nszt.', 16, 50, 90, 23, 72, false), fit: [25, 35], rowSpan: 2, margin: [0, 0, 0, -15] },
            { text: 'szt.', rowSpan: 2, alignment: 'center', fontSize: 9, margin:[0, 12, 0, -15] },
            { text: 'szt.', rowSpan: 2, alignment: 'center', fontSize: 9, margin: [0, 12, 0, -15]  },
            { text: 'szt.', rowSpan: 2, alignment: 'center', fontSize: 9, margin: [0, 12, 0, -15]  },
            { text: 'odstrzał szt.', rowSpan: 2, alignment: 'center', fontSize: 9, margin: [0, 10, 0, -13] },
            { text: 'odłów szt.', rowSpan: 2, alignment: 'center', fontSize: 9, margin: [0, 10, 0, -13] },
            { text: 'odstrzał szt.', colSpan: 2, alignment: 'center', fontSize: 8, margin: [-3, 0, -3, 0] },
            { text: '' },
            { text: 'odłów szt.', colSpan: 2, alignment: 'center', fontSize: 8 },
            { text: '' }
        ]);

        headers.push([
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { image: drawString('min', 16, 20, 60, 16, 50), fit: [10, 20], alignment: 'center' },
            { image: drawString('max', 16, 20, 60, 16, 50), fit: [10, 20], alignment: 'center' },
            { image: drawString('min', 16, 20, 60, 16, 50), fit: [10, 20], alignment: 'center' },
            { image: drawString('max', 16, 20, 60, 16, 50), fit: [10, 20], alignment: 'center' },
        ]);

        headers.push(getNumbersRow(14));

        return headers;
    }

    var getSmallGameTableBody = function (smallGamePlanModel) {
        var smallGameTableBody = [];
        
        $.each(smallGamePlanModel.AnnualPlanKindGameModels, function (key, gameKindModel) {

            if (gameKindModel.AnnualPlanSubKindGameModels.length === 0) {
                smallGameTableBody.push([
                    { text: gameKindModel.KindName, bold: true, style: 'point', margin: [0, -2, 0, -1] },
                    { text: gameKindModel.PreviousHuntPlanCulls, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: gameKindModel.PreviousHuntPlanCatches, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: gameKindModel.PreviousHuntPlanExecutionCulls, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: gameKindModel.PreviousHuntPlanExecutionCatches, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: '' },
                    { text: gameKindModel.GameCountBefore10March, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: '' },
                    { text: gameKindModel.CurrentHuntPlanCulls, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: gameKindModel.CurrentHuntPlanCatches, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: gameKindModel.CurrentHuntPlanCullsMin, style: 'cell', margin: [0, -2, 0, -1]  },
                    { text: gameKindModel.CurrentHuntPlanCullsMax, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: gameKindModel.CurrentHuntPlanCatchesMin, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: gameKindModel.CurrentHuntPlanCatchesMax, style: 'cell', margin: [0, -2, 0, -1]  }
                ]);
            }
            
            $.each(gameKindModel.AnnualPlanSubKindGameModels, function (index, gameSubKindModel) {

                smallGameTableBody.push([
                    { text: gameKindModel.KindName + ' ' + gameSubKindModel.SubKindName, bold: true, style: 'point', margin: [0, -2, 0, -1] },
                    { text: gameSubKindModel.PreviousHuntPlanCulls, style: 'cell', margin: [0, -2, 0, -1]  },
                    { text: gameSubKindModel.PreviousHuntPlanCatches, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: gameSubKindModel.PreviousHuntPlanExecutionCulls, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: gameSubKindModel.PreviousHuntPlanExecutionCatches, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: '' },
                    { text: gameSubKindModel.GameCountBefore10March, style: 'cell', margin: [0, -2, 0, -1]  },
                    { text: '' },
                    { text: gameSubKindModel.CurrentHuntPlanCulls, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: gameSubKindModel.CurrentHuntPlanCatches, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: gameSubKindModel.CurrentHuntPlanCullsMin, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: gameSubKindModel.CurrentHuntPlanCullsMax, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: gameSubKindModel.CurrentHuntPlanCatchesMin, style: 'cell', margin: [0, -2, 0, -1] },
                    { text: gameSubKindModel.CurrentHuntPlanCatchesMax, style: 'cell', margin: [0, -2, 0, -1] }
                ]);
            });
        });

        return smallGameTableBody;
    }

    var getEconomyTable = function (data) {
        var economyTableHeaders = getEconomyTableHeaders();
        var economyTableNumberColumns = getNumbersRow(economyTableHeaders.length);
        var economyTableBody = getEconomyTableBody(data.PreviousAnnualPlanModel, data.CurrentAnnualPlanModel);

        var economyTable = [];
        economyTable.push(economyTableHeaders);
        economyTable.push(economyTableNumberColumns);
        for (var i = 0; i < economyTableBody.length; i++) {
            economyTable.push(economyTableBody[i]);
        }

        return economyTable;
    };

    var getCostTable = function (data) {
        var costTableNumberColumns = getNumbersRow(6);
        var costTableBody = getRevenueTableBody(data.PreviousAnnualPlanModel, data.CurrentAnnualPlanModel);

        var costTable = [];
        costTable.push(costTableNumberColumns);
        for (var i = 0; i < costTableBody.length; i++) {
            costTable.push(costTableBody[i]);
        }

        return costTable;
    };

    var getBigGameTable = function (data) {
        var bigGameTableHeaders = getBigGameTableHeaders();
        var bigGameTableBody = getBigGameTableBody(data.BigGamePlanModel);

        var bigGameTable = [];
        for (let i = 0; i < bigGameTableHeaders.length; i++) {
            bigGameTable.push(bigGameTableHeaders[i]);
        }
        for (let i = 0; i < bigGameTableBody.length; i++) {
            bigGameTable.push(bigGameTableBody[i]);
        }

        return bigGameTable;
    };

    var getSmallGameTable = function (data) {
        var smallGameTableHeaders = getSmallGameTableHeaders();
        var smallGameTableBody = getSmallGameTableBody(data.SmallGamePlanModel);

        var smallGameTable = [];
        for (let i = 0; i < smallGameTableHeaders.length; i++) {
            smallGameTable.push(smallGameTableHeaders[i]);
        }
        for (let i = 0; i < smallGameTableBody.length; i++) {
            smallGameTable.push(smallGameTableBody[i]);
        }

        return smallGameTable;
    };


    var getApprovalsSection = function () {
        var approvalsSection = [];

        approvalsSection.push({ text: 'a) uzgodnienia', fontSize: 12, margin: [0, 20, 0, 60] });
        approvalsSection.push({ text: '..........................................................................................................................................   .......................................', fontSize: 11, margin: [25, 0, 0, 0] });
        approvalsSection.push({ text: 'Polski Związek Łowiecki                                                                           (data, podpis)', fontSize: 9, margin: [180, 0, 0, 0] });
        approvalsSection.push({ text: 'b) opinie', fontSize: 12, margin: [0, 40, 0, 20] });

        approvalsSection.push({ text: '1. Wójt (burmistrz, prezydent miasta)', fontSize: 12 });
        getDottedRows(approvalsSection, 10);
        getApprovalsSectionSignRows(approvalsSection);

        approvalsSection.push({ text: '2. Izba rolnicza', fontSize: 12 });
        getDottedRows(approvalsSection, 9);
        getApprovalsSectionSignRows(approvalsSection);

        approvalsSection.push({ text: '3. Polski Związek Łowiecki*', fontSize: 12, pageBreak: 'before'});
        getDottedRows(approvalsSection, 3);
        getApprovalsSectionSignRows(approvalsSection);
        
        approvalsSection.push({ text: '4. Dyrektor parku narodowego**', fontSize: 12 });
        getDottedRows(approvalsSection, 3);
        getApprovalsSectionSignRows(approvalsSection);

        approvalsSection.push({ text: '5. Opinia uprawnionych do rybactwa***', fontSize: 12 });
        getDottedRows(approvalsSection, 3);
        getApprovalsSectionSignRows(approvalsSection);

        approvalsSection.push({ text: '*dla obwodów wyłączonych z wydzierżawienia', fontSize: 7 });
        approvalsSection.push({ text: '**dla obwodów graniczących z parkiem narodowym', fontSize: 7 });
        approvalsSection.push({ text: '***dla obwodów na terenie których znajduje się obręb hodowlany', fontSize: 7 });

        return approvalsSection;
    };

    var downloadPdf = function (data) {
        var planHeader = getPlanHeader();
        var huntClubInformation = getHuntClubInformation();
        var economyTable = getEconomyTable(data);
        var costTable = getCostTable(data);
        var bigGameTable = getBigGameTable(data);
        var smallGameTable = getSmallGameTable(data);
        var approvalsSection = getApprovalsSection();

        pdfMake.fonts = {
            TimesNewRoman: {
                normal: 'times.ttf',
                bold: 'timesbd.ttf',
                italics: 'timesi.ttf',
                bolditalics: 'timesbi.ttf'
            }
        }

        var docDefinition = {
            pageOrientation: 'portrait',
            pageMargins: [28, 28, 28, 28],
            pageSize: 'A4',
            content: [
                   planHeader
                ,
                {
                    text: 'I. Dane ogólne',
                    style: 'planPoint'
                },
                    huntClubInformation
                ,
                {
                    text: 'II. Dane dotyczące zagospodarowania obwodu łowieckiego oraz szkód łowieckich',
                    style: 'planPoint', margin: [0, 0, 0, 10],
                    pageBreak: 'before'
                },
                {
                    table: {
                        widths: ['48%', '5%', '12%', '12%', '11%', '12%'],
                        body: economyTable,
                        pageBreak: 'after',
                        dontBreakRows: true
                    },
                    layout: {
                        hLineWidth: function (i, node) {
                            return (i === 0 || i === node.table.body.length) ? 3 : 1;
                        },
                        vLineWidth: function (i, node) {
                            return (i === 0 || i === node.table.widths.length) ? 3 : 1;
                        }
                    }
                },
                {
                    text: 'III. Informacja o przychodach ze sprzedaży tusz zwierzyny płowej',
                    fontSize: 12, bold: true, margin: [0, 20, 0, 20]
                },
                {
                    table: {
                        widths: ['47%', '5%', '12%', '12%', '12%', '12%'],
                        body: costTable,
                        pageBreak: 'before',
                        dontBreakRows: true
                    },
                    layout: {
                        hLineWidth: function (i, node) {
                            return (i === 0 || i === node.table.body.length) ? 3 : 1;
                        },
                        vLineWidth: function (i, node) {
                            return (i === 0 || i === node.table.widths.length) ? 3 : 1;
                        }
                    }
                },
                {
                    text: 'IV. Dane dotyczące zwierząt łownych w obwodzie łowieckim',
                    style: 'planPoint', margin: [0, 0, 0, 10], pageBreak: 'before'
                },
                {
                    text: 'a) zwierzyna gruba',
                    fontSize: 11, bold: true, margin: [0, 0, 0, 10]
                },
                {
                    table: {
                        widths: ['20%', '4%', '4%', '4%', '4%', '4%', '5%', '6%', '8%', '7%', '4%', '9%', '5%', '5%', '3%', '3%', '3%', '3%'],
                        body: bigGameTable,
                        pageBreak: 'before',
                        dontBreakRows: true
                    },
                    layout: {
                        hLineWidth: function(i, node) {
                            return (i === 0 || i === node.table.body.length) ? 3 : 1;
                        },
                        vLineWidth: function(i, node) {
                            return (i === 0 || i === node.table.widths.length) ? 3 : 1;
                        }
                    }
                },
                {
                    text: 'b) zwierzyna drobna',
                    fontSize: 11, bold: true, margin: [0, 0, 0, 20], pageBreak: 'before'
                },
                {
                    table: {
                        widths: ['25%', '6%', '6%', '6%', '6%', '12%', '8%', '5%', '7%', '7%', '3%', '3%', '3%', '3%'],
                        body: smallGameTable,
                        pageBreak: 'before',
                        dontBreakRows: true
                    },
                    layout: {
                        hLineWidth: function (i, node) {
                            return (i === 0 || i === node.table.body.length) ? 3 : 1;
                        },
                        vLineWidth: function (i, node) {
                            return (i === 0 || i === node.table.widths.length) ? 3 : 1;
                        }
                    }
                },
                {
                    text: 'V. Uzgodnienia i opinie',
                    style: 'planPoint', margin: [0, 0, 0, 10], pageBreak: 'before'
                },
                    approvalsSection
            ],
            footer: function (page) {
                return { text: page, alignment: 'center', fontSize: 10 };
            },
            defaultStyle: {
                font: 'TimesNewRoman'
            },
            styles: {
                planPoint: {
                    fontSize: 12,
                    bold: true
                },
                header: {
                    alignment: 'center',
                    fontSize: 9,
                    bold: true
                },
                numberHeader: {
                    alignment: 'center',
                    fontSize: 7
                },
                paragraph: {
                    fontSize: 10,
                    bold: true
                },
                point: {
                    fontSize: 10,
                    alignment: 'left'
                },
                smallPoint: {
                    fontSize: 8,
                    alignment: 'left'
                },
                cell: {
                    fontSize: 9,
                    alignment: 'center'
                },
                smallCell: {
                    fontSize: 8,
                    alignment: 'center'
                }
            }
        };

        pdfMake.createPdf(docDefinition).download('Roczny Plan Łowiecki ' + currentMarketingYearModel.MarketingYear + '.pdf');
    };
    
    $('#generateAnnualPlanPdf').on('click', function () {
        $.ajax({
            type: "GET",
            url: '/AnnualPlan/GetAnnualPlanJsonData?marketingYearId=' + currentMarketingYearModel.Id,
            success: function (data) {
                downloadPdf(data);
            },
            error: function () {
                alert('Coś poszło nie tak, proszę odświeżyć stronę.');
            }
        });
    });
};