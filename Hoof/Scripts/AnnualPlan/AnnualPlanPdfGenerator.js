var AnnualPlanGenerator = function (config) {

    var marketingYearId = config.marketingYearId;
    var kindCount;

    var writeRotatedText = function (text) {
        var ctx, canvas = document.createElement('canvas');
        // I am using predefined dimensions so either make this part of the arguments or change at will 
        canvas.width = 50;
        canvas.height = 130;
        ctx = canvas.getContext('2d');
        ctx.font = '9pt Times New Roman';
        ctx.save();
        ctx.translate(36, 270);
        ctx.rotate(-0.5 * Math.PI);
        ctx.fillStyle = '#000';
        ctx.fillText(text, 0, 0);
        ctx.restore();
        return canvas.toDataURL();
    };

    var getNumbersRow = function (columnsNumber) {
        var numbersRow = [];
        for (var i = 1; i <= columnsNumber; i++) {
            numbersRow.push({ text: i, style: 'numberHeader' });
        }
        return numbersRow;
    }

    var getPlanHeader = function () {
        var header = [];

        header.push({ text: 'ROCZNY PLAN ŁOWIECKI', alignment: 'center', fontSize: 14, bold: true });

        header.push({ text: 'na rok gospodarczy ...................../.....................', alignment: 'center', fontSize: 9, bold: true, margin: [0, 10, 0, 0] });

        header.push({ text: 'oraz sprawozdanie z wykonania planu roku gospodarczego ......................./…..................', alignment: 'center', fontSize: 9, bold: true, margin: [0, 5, 0, 10] });

        return header;
    }

    var getHuntClubInformation = function () {
        var huntClubInformation = [];

        huntClubInformation.push({
            text: [
                { text: '1. Obwód łowiecki nr ', fontSize: 10 },
                { text: '20 ', fontSize: 10, bold: true },
                { text: 'powierzchnia ', fontSize: 10 },
                { text: '4777 ', fontSize: 10, bold: true},
                { text: 'ha, w tym powierzchnia gruntów leśnych ', fontSize: 10 },
                { text: '4394 ', fontSize: 10, bold: true},
                { text: 'ha powierzchnia po wyłączeniach, o których mowa w art. 26 ustawy z 13.X.1995r.Prawo Łowieckie ', fontSize: 10 },
                { text: '4502 ', fontSize: 10, bold: true },
                { text: 'ha', fontSize: 10 }
            ], margin: [0, 5, 0, 0]
        });

        huntClubInformation.push({
            text: [
                { text: '2. Województwo ', fontSize: 10 },
                { text: 'wielkopolskie, ', fontSize: 10, bold: true},
                { text: 'Powiat ', fontSize: 10},
                { text: 'złotowski', fontSize: 10, bold: true }
            ], margin: [0, 5, 0, 0]
            
        });

        huntClubInformation.push({
            text: [
                { text: '3. Nadleśnictwo (nazwa i adres siedziby) ', fontSize: 10 },
                { text: 'Płytnica z siedzibą w Nowej Szwecji, Nowa Szwecja 6, 78-600 Wałcz', fontSize: 10, bold: true }
            ], margin: [0, 5, 0, 0]
        });

        huntClubInformation.push({
            text: [
                { text: '4. Regionalna Dyrekcja Lasów Państwowych (nazwa i adres siedziby) ', fontSize: 10 },
                { text: 'Piła, 64-920 Piła-Kalina', fontSize: 10, bold: true}
            ], margin: [0, 5, 0, 0]
        });

        huntClubInformation.push({
            text: [
                { text: '5. Zarząd Okręgowy PZŁ (nazwa i adres siedziby) ', fontSize: 10},
                { text: 'Piła, Al. Powstańców Wielkopolskich 190, 64-920 Piła', fontSize: 10, bold: true }
            ], margin: [0, 5, 0, 0]
        });

        huntClubInformation.push({
            text: [
                { text: '6. Dzierżawca/lub zarządca (nazwa i adres siedziby) ', fontSize: 10},
                { text: 'Koło Łowieckie nr 39 Literatów Polskich „Pióro” ul. Rostworowskiego 6/2, 01-496 Warszawa', fontSize: 10, bold: true }
            ], margin: [0, 5, 0, 10] 
        });

        return huntClubInformation;
    }
    
    var getEconomyTableHeaders = function() {
        var headers = [];

        headers.push({ text: 'Wyszczególnienie', style: 'header', margin: [0, 32, 0, 0] });
        headers.push({ text: 'Jedn. miary', style: 'header', margin: [0, 25, 0, 0] });
        headers.push({ text: 'Plan poprzedniego roku gospodarczego .../...', style: 'header', margin: [0, 10, 0, 0]});
        headers.push({ text: 'Wykonanie planu poprzedniego roku gospodarczego .../...', style: 'header' });
        headers.push({ text: 'Stan na 10 marca roku, na który sporządza się plan ... r.', style: 'header', margin: [0, 10, 0, 0]});
        headers.push({ text: 'Stan planowany do osiągnięcia w bieżącym roku gospodarczym .../...', style: 'header'});

        return headers;
    };

    var getEconomyTableBody = function (previousAnnualPlanModel, currentAnnualPlanModel) {
        var body = []; 

        body.push([
            { text: '1. Liczba osób zatrudnionych w oparciu o umowę o pracę w celu wykonywania zadań z zakresu gospodarki łowieckiej', style: ['paragraph', ''] },
            { text: 'osoby/etaty', margin: [0, 5, 0, 0], style: 'cell' },
            { text: previousAnnualPlanModel.EmployeePlanModel.FullTimeEmployees, style: 'cell', margin:[0, 10, 0, 0]},
            { text: '', style: 'cell'},
            { text: '', style: 'cell'},
            { text: currentAnnualPlanModel.EmployeePlanModel.FullTimeEmployees, style: 'cell', margin: [0, 10, 0, 0]}
        ]);

        body.push([
            { text: '2. Liczba osób zatrudnionych na innej podstawie niż umowa o pracę, lub powołanych, w celu wykonywania zadań z zakresu gospodarki łowieckiej', style: 'paragraph' },
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

        for (let property in currentAnnualPlanModel.HuntEquipmentPlanModel) {
            if (currentAnnualPlanModel.HuntEquipmentPlanModel.hasOwnProperty(property)) {
                body.push([
                    { text: property, style: 'point' },
                    { text: 'szt.', style: 'cell' },
                    { text: previousAnnualPlanModel.HuntEquipmentPlanModel[property], style: 'cell' },
                    { text: '', style: 'cell' },
                    { text: '', style: 'cell' },
                    { text: currentAnnualPlanModel.HuntEquipmentPlanModel[property], style: 'cell' }
                ]);
            }
        }

        body.push([
            { text: '4. Poletka łowieckie(obszary obsiane lub obsadzone roślinami stanowiącymi żer dla zwierzyny na pniu)', style: 'paragraph' },
            { text: 'ha', margin: [0, 5, 0, 0], style: 'cell' },
            { text: previousAnnualPlanModel.TrunkFoodPlanModel.Hectare, style: 'cell', margin: [0, 5, 0, 0] },
            { text: '', style: 'cell' },
            { text: '', style: 'cell' },
            { text: currentAnnualPlanModel.TrunkFoodPlanModel.Hectare, style: 'cell', margin: [0, 5, 0, 0] }
        ]);

        body.push([
            { text: '5. Pasy zaporowe', style: 'paragraph' },
            { text: 'km', style: 'cell' },
            { text: previousAnnualPlanModel.BarrierPlanModel.Length, style: 'cell' },
            { text: '', style: 'cell' },
            { text: '', style: 'cell' },
            { text: currentAnnualPlanModel.BarrierPlanModel.Length, style: 'cell' }
        ]);

        body.push([
            { text: '6. Zagospodarowane przez dzierżawcę lub zarządcę łąki śródleśne i przyleśne', style: 'paragraph' },
            { text: 'ha', margin: [0, 5, 0, 0], style: 'cell' },
            { text: previousAnnualPlanModel.FieldPlanModel.Hectare, style: 'cell', margin: [0, 5, 0, 0] },
            { text: '', style: 'cell' },
            { text: '', style: 'cell' },
            { text: currentAnnualPlanModel.FieldPlanModel.Hectare, style: 'cell', margin: [0, 5, 0, 0] }
        ]);

        body.push([
            { text: '7. Karma i sól.', style: 'paragraph' },
            { text: 'X', style: 'cell' },
            { text: 'X', style: 'cell' },
            { text: 'X', style: 'cell' },
            { text: 'X', style: 'cell' },
            { text: 'X', style: 'cell' }
        ]);

        for (let property in currentAnnualPlanModel.FodderPlanModel) {
            if (currentAnnualPlanModel.FodderPlanModel.hasOwnProperty(property)) {
                body.push([
                    { text: property, style: 'point' },
                    { text: 'tona', style: 'cell' },
                    { text: previousAnnualPlanModel.FodderPlanModel[property], style: 'cell' },
                    { text: '', style: 'cell' },
                    { text: '', style: 'cell' },
                    { text: currentAnnualPlanModel.FodderPlanModel[property], style: 'cell' }
                ]);
            }
        }

        body.push([
            { text: '8. Powierzchnia zredukowana upraw rolnych uszkodzonych przez zwierzęta łowne', style: 'paragraph' },
            { text: 'ha', margin: [0, 5, 0, 0], style: 'cell' },
            { text: previousAnnualPlanModel.DamagedFieldPlanModel.Hectare, style: 'cell', margin: [0, 5, 0, 0] },
            { text: '', style: 'cell' },
            { text: '', style: 'cell' },
            { text: currentAnnualPlanModel.DamagedFieldPlanModel.Hectare, style: 'cell', margin: [0, 5, 0, 0] }
        ]);

        return body;
    }
    
    var getCostTableBody = function (previousAnnualPlanModel, currentAnnualPlanModel) {
        var body = [];

        body.push([
            { text: '1. Koszty poniesione na prowadzenie gospodarki łowieckiej', style: 'paragraph' },
            { text: 'tyś. zł', margin: [0, 5, 0, 0], style: 'cell' },
            { text: previousAnnualPlanModel.CostPlanModel.Cost, style: 'cell', margin: [0, 5, 0, 0] },
            { text: '', style: 'cell' },
            { text: '', style: 'cell' },
            { text: currentAnnualPlanModel.CostPlanModel.Cost, style: 'cell', margin: [0, 5, 0, 0] }
        ]);

        body.push([
            { text: 'w tym : kwota wypłaconych odszkodowań łowieckich', style: 'paragraph' },
            { text: 'tyś. zł', style: 'cell' },
            { text: '', style: 'cell' },
            { text: '', style: 'cell' },
            { text: '', style: 'cell' },
            { text: '', style: 'cell' }
        ]);

        body.push([
            { text: '2. Przychody ze sprzedaży tusz zwierzyny płowej', style: 'paragraph' },
            { text: 'tyś. zł', style: 'cell' },
            { text: previousAnnualPlanModel.CostPlanModel.Revenue, style: 'cell' },
            { text: '', style: 'cell' },
            { text: '', style: 'cell' },
            { text: currentAnnualPlanModel.CostPlanModel.Revenue, style: 'cell' }
        ]);

        return body;
    }

    var getBigGameTableHeaders = function() {
        var headers = [];

        headers.push([
            { text: 'Gatunki zwierząt łownych', rowSpan: 4, style: 'numberHeader' },
            { text: 'Plan pozyskania roku poprzedniego .……/…….', colSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: '' },
            { text: 'Wykonanie planu pozyskania roku poprzedniego ……/…….', colSpan: 4, alignment: 'center', style: 'numberHeader' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: 'Odstrzał sanitarny w poprzednim roku gospodarczym', alignment: 'center', style: 'numberHeader' },
            { text: 'Liczba zasiedlonych zwierząt do 10.03 poprzedniego roku gosp.', alignment: 'center', style: 'numberHeader' },
            { text: 'Szacowana liczebność zwierząt na 10.03 ………r *', alignment: 'center', style: 'numberHeader' },
            { text: 'Plan zasiedleń w roku gosp.……/…..', alignment: 'center', style: 'numberHeader' },
            { text: 'Planowana liczebność zwierzyny grubej przed okresem polowań*', alignment: 'center', style: 'numberHeader' },
            { text: 'Optymalna liczba zwierząt zaplanowanych do pozyskania w …….. /……. roku gospodarczym', colSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: '' },
            { text: 'Minimalna i maksymalna liczba zwierząt zaplanowana do pozyskania w roku gospodarczym ..……./………', colSpan: 4, alignment: 'center', style: 'numberHeader' },
            { text: '' },
            { text: '' },
            { text: '' }
        ]);

        headers.push([
            { text: '' },
            { text: 'odstrzał szt.', rowSpan: 3, alignment: 'center', style: 'numberHeader' },
            { text: 'odłów szt.', rowSpan: 3, alignment: 'center', style: 'numberHeader' },
            { text: 'ogółem szt.', rowSpan: 3, alignment: 'center', style: 'numberHeader' },
            { text: 'w tym szt.', colSpan: 3, alignment: 'center', style: 'numberHeader' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: 'szt.', rowSpan: 3, alignment: 'center', style: 'numberHeader' },
            { text: 'szt.', rowSpan: 3, alignment: 'center', style: 'numberHeader'},
            { text: 'szt.', rowSpan: 3, alignment: 'center', style: 'numberHeader'},
            { text: 'szt.', rowSpan: 3, alignment: 'center', style: 'numberHeader'},
            { text: 'odstrzał szt.', rowSpan: 3, alignment: 'center', style: 'numberHeader'},
            { text: 'odłów szt.', rowSpan: 3, alignment: 'center', style: 'numberHeader' },
            { text: 'odstrzał szt.', colSpan: 2, alignment: 'center', style: 'numberHeader'},
            { text: '' },
            { text: 'odłów szt.', colSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: '' }
        ]);

        headers.push([
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: 'odstrzał', rowSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: 'odłów ', rowSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: 'ubytki', colSpan: 2, alignment: 'center', style: 'numberHeader'},
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: 'min', rowSpan: 2, alignment: 'center', style: 'numberHeader'},
            { text: 'max', rowSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: 'min', rowSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: 'max', rowSpan: 2, alignment: 'center', style: 'numberHeader'}
        ]);

        headers.push([
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: 'ogółem', alignment: 'center', fontSize: 5 },
            { text: 'W tym odstrzał sanitarny', alignment: 'center', fontSize: 5 },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' },
            { text: '' }
        ]);

        headers.push(getNumbersRow(18));

        return headers;
    }

    var getBigGameTableBody = function (bigGamePlanModel) {
        var bigGameTableBody = [];

        kindCount = 1;
        $.each(bigGamePlanModel.AnnualPlanKindGameModels, function(key, value) {
            
            bigGameTableBody.push([
                { text: kindCount + '. ' + value.KindName, bold: true, style: 'point'},
                { text: value.PreviousHuntPlanCulls, style: 'cell'},
                { text: value.PreviousHuntPlanCatches, style: 'cell'},
                { text: value.PreviousHuntPlanExecutionTotal, style: 'cell'},
                { text: value.PreviousHuntPlanExecutionCulls, style: 'cell'},
                { text: value.PreviousHuntPlanExecutionCatches, style: 'cell'},
                { text: value.PreviousHuntPlanExecutionLosses, style: 'cell'},
                { text: value.PreviousHuntPlanExecutionSanitaryLosses, style: 'cell'},
                { text: '' },
                { text: value.GameCountBefore10March, style: 'cell'},
                { text: '' },
                { text: '' },
                { text: value.CurrentHuntPlanCulls, style: 'cell' },
                { text: value.CurrentHuntPlanCatches, style: 'cell'},
                { text: value.CurrentHuntPlanCullsMin, style: 'cell'},
                { text: value.CurrentHuntPlanCullsMax, style: 'cell'},
                { text: value.CurrentHuntPlanCatchesMin, style: 'cell'},
                { text: value.CurrentHuntPlanCatchesMax, style: 'cell' }
            ]);

            $.each(value.AnnualPlanSubKindGameModels, function (index, value) {

                bigGameTableBody.push([
                    { text: '-' + value.SubKindName, style: 'point'},
                    { text: value.PreviousHuntPlanCulls, style: 'cell'},
                    { text: value.PreviousHuntPlanCatches, style: 'cell'},
                    { text: value.PreviousHuntPlanExecutionTotal, style: 'cell'},
                    { text: value.PreviousHuntPlanExecutionCulls, style: 'cell'},
                    { text: value.PreviousHuntPlanExecutionCatches, style: 'cell'},
                    { text: value.PreviousHuntPlanExecutionLosses, style: 'cell'},
                    { text: value.PreviousHuntPlanExecutionSanitaryLosses, style: 'cell'},
                    { text: '' },
                    { text: value.GameCountBefore10March, style: 'cell'},
                    { text: '' },
                    { text: '' },
                    { text: value.CurrentHuntPlanCulls, style: 'cell'},
                    { text: value.CurrentHuntPlanCatches, style: 'cell'},
                    { text: value.CurrentHuntPlanCullsMin, style: 'cell'},
                    { text: value.CurrentHuntPlanCullsMax, style: 'cell'},
                    { text: value.CurrentHuntPlanCatchesMin, style: 'cell'},
                    { text: value.CurrentHuntPlanCatchesMax, style: 'cell' }
                ]);

                $.each(value.AnnualPlanClassGameModels, function (index, value) {

                    bigGameTableBody.push([
                        { text: '-' + value.ClassName, style: 'cell'},
                        { text: value.PreviousHuntPlanCulls, style: 'cell'},
                        { text: value.PreviousHuntPlanCatches, style: 'cell'},
                        { text: value.PreviousHuntPlanExecutionTotal, style: 'cell'},
                        { text: value.PreviousHuntPlanExecutionCulls, style: 'cell'},
                        { text: value.PreviousHuntPlanExecutionCatches, style: 'cell'},
                        { text: value.PreviousHuntPlanExecutionLosses, style: 'cell'},
                        { text: value.PreviousHuntPlanExecutionSanitaryLosses, style: 'cell'},
                        { text: '' },
                        { text: value.GameCountBefore10March, style: 'cell'},
                        { text: '' },
                        { text: '' },
                        { text: value.CurrentHuntPlanCulls, style: 'cell'},
                        { text: value.CurrentHuntPlanCatches, style: 'cell'},
                        { text: value.CurrentHuntPlanCullsMin, style: 'cell'},
                        { text: value.CurrentHuntPlanCullsMax, style: 'cell'},
                        { text: value.CurrentHuntPlanCatchesMin, style: 'cell'},
                        { text: value.CurrentHuntPlanCatchesMax, style: 'cell'}
                    ]);
                });
            });
            kindCount++;
        });

        return bigGameTableBody;
    }

    var getSmallGameTableHeaders = function() {
        var headers = [];

        headers.push([
            { text: 'Gatunki zwierząt łownych', rowSpan: 3, style: 'numberHeader' },
            { text: 'Plan pozyskania roku poprzedniego .……/…….', colSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: '' },
            { text: 'Wykonanie planu pozyskania roku poprzedniego ……/…….', colSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: '' },
            { text: 'Liczba zasiedlonych zwierząt do 10.03 poprzedniego roku gosp.', alignment: 'center', style: 'numberHeader' },
            { text: 'Szacowana liczebność zwierząt na 10.03 ………r *', alignment: 'center', style: 'numberHeader' },
            { text: 'Plan zasiedleń w roku gosp.……/…..', alignment: 'center', style: 'numberHeader' },
            { text: 'Optymalna liczba zwierząt zaplanowanych do pozyskania w …….. /……. roku gospodarczym', colSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: '' },
            { text: 'Minimalna i maksymalna liczba zwierząt zaplanowana do pozyskania w roku gospodarczym ..……./………', colSpan: 4, alignment: 'center', style: 'numberHeader' },
            { text: '' },
            { text: '' },
            { text: '' }
        ]);

        headers.push([
            { text: '' },
            { text: 'odstrzał szt.', rowSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: 'odłów szt.', rowSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: 'odstrzał szt.', rowSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: 'odłów szt.', rowSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: 'szt.', rowSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: 'szt.', rowSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: 'szt.', rowSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: 'odstrzał szt.', rowSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: 'odłów szt.', rowSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: 'odstrzał szt.', colSpan: 2, alignment: 'center', style: 'numberHeader' },
            { text: '' },
            { text: 'odłów szt.', colSpan: 2, alignment: 'center', style: 'numberHeader' },
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
            { text: 'min', alignment: 'center', style: 'numberHeader' },
            { text: 'max', alignment: 'center', style: 'numberHeader' },
            { text: 'min', alignment: 'center', style: 'numberHeader' },
            { text: 'max', alignment: 'center', style: 'numberHeader' }
        ]);

        headers.push(getNumbersRow(14));

        return headers;
    }

    var getSmallGameTableBody = function (smallGamePlanModel) {
        var smallGameTableBody = [];
        
        $.each(smallGamePlanModel.AnnualPlanKindGameModels, function (key, value) {

            smallGameTableBody.push([
                { text: kindCount + '. ' + value.KindName, bold: true, style: 'point' },
                { text: value.PreviousHuntPlanCulls, style: 'cell' },
                { text: value.PreviousHuntPlanCatches, style: 'cell' },
                { text: value.PreviousHuntPlanExecutionCulls, style: 'cell' },
                { text: value.PreviousHuntPlanExecutionCatches, style: 'cell' },
                { text: '' },
                { text: value.GameCountBefore10March, style: 'cell' },
                { text: '' },
                { text: value.CurrentHuntPlanCulls, style: 'cell' },
                { text: value.CurrentHuntPlanCatches, style: 'cell' },
                { text: value.CurrentHuntPlanCullsMin, style: 'cell' },
                { text: value.CurrentHuntPlanCullsMax, style: 'cell' },
                { text: value.CurrentHuntPlanCatchesMin, style: 'cell' },
                { text: value.CurrentHuntPlanCatchesMax, style: 'cell' }
            ]);

            $.each(value.AnnualPlanSubKindGameModels, function (index, value) {

                smallGameTableBody.push([
                    { text: '-' + value.SubKindName, style: 'cell' },
                    { text: value.PreviousHuntPlanCulls, style: 'cell' },
                    { text: value.PreviousHuntPlanCatches, style: 'cell' },
                    { text: value.PreviousHuntPlanExecutionCulls, style: 'cell' },
                    { text: value.PreviousHuntPlanExecutionCatches, style: 'cell' },
                    { text: '' },
                    { text: value.GameCountBefore10March, style: 'cell' },
                    { text: '' },
                    { text: value.CurrentHuntPlanCulls, style: 'cell' },
                    { text: value.CurrentHuntPlanCatches, style: 'cell' },
                    { text: value.CurrentHuntPlanCullsMin, style: 'cell' },
                    { text: value.CurrentHuntPlanCullsMax, style: 'cell' },
                    { text: value.CurrentHuntPlanCatchesMin, style: 'cell' },
                    { text: value.CurrentHuntPlanCatchesMax, style: 'cell' }
                ]);
            });
            kindCount++;
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
        var costTableBody = getCostTableBody(data.PreviousAnnualPlanModel, data.CurrentAnnualPlanModel);

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

    var getSignsAndApprovalsSection = function() {
        var signsAndApprovalsContent = [];

        signsAndApprovalsContent.push({ text: 'Data sporządzenia planu:', fontSize: 8, bold: true, margin:[0, 5, 0, 0]});
        signsAndApprovalsContent.push({ text: 'Plan sporządził:                                                           Podpis ...............................................', fontSize: 8, bold: true, margin: [0, 5, 0, 0]});
        signsAndApprovalsContent.push({ text: 'Opinia Wójta, Burmistrza lub Prezydenta miasta....................................................................................................................................................................', fontSize: 8, bold: true, margin: [0, 5, 0, 0]});
        signsAndApprovalsContent.push({ text: '................................................................................................................................................................................................................................................................', fontSize: 8, margin: [0, 5, 0, 0]});
        signsAndApprovalsContent.push({ text: '................................', alignment: 'right', fontSize: 8, margin: [0, 5, 0, 0]});
        signsAndApprovalsContent.push({ text: '(data, podpis)', alignment: 'right', fontSize: 8});
        signsAndApprovalsContent.push({ text: 'Inne opinie, o których mowa w art.8 ust. 3c', fontSize: 8});
        signsAndApprovalsContent.push({ text: 'ustawy z dnia 13 października 1995 r.– Prawo łowieckie', fontSize: 8});
        signsAndApprovalsContent.push({ text: '................................................................................................................................................................................................................................................................', fontSize: 8, margin: [0, 5, 0, 0]});
        signsAndApprovalsContent.push({ text: '................................................................................................................................................................................................................................................................', fontSize: 8, margin: [0, 5, 0, 0]});
        signsAndApprovalsContent.push({ text: '................................', alignment: 'right', fontSize: 8, margin: [0, 5, 0, 0]});
        signsAndApprovalsContent.push({ text: '(data, podpis)', alignment: 'right', fontSize: 8});
        signsAndApprovalsContent.push({ text: 'Plan uzgodnił:                                                                                                                                                                                                                     Plan zatwierdził:', fontSize: 8, bold: true, margin: [0, 10, 0, 0]});
        signsAndApprovalsContent.push({ text: '...................................................                                                                                                                                                                   ...................................................', fontSize: 8, margin: [0, 5, 0, 0] });
        signsAndApprovalsContent.push({ text: '(data, podpis, pieczęć)                                                                                                                                                                                               (data, podpis, pieczęć)', fontSize: 8 });
        
        return signsAndApprovalsContent;
    }

    var downloadPdf = function (data) {

        var planHeader = getPlanHeader();
        var huntClubInformation = getHuntClubInformation();
        var economyTable = getEconomyTable(data);
        var costTable = getCostTable(data);
        var bigGameTable = getBigGameTable(data);
        var smallGameTable = getSmallGameTable(data);
        var signsAndApprovalsSection = getSignsAndApprovalsSection();
        
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
                    text: 'II. Zagospodarowanie obwodu łowieckiego, szkody łowieckie',
                    style: 'planPoint'
                },
                {
                    table: {
                        widths: ['47%', '5%', '12%', '12%', '12%', '12%'],
                        body: economyTable,
                        pageBreak: 'before',
                        dontBreakRows: true
                    }
                },
                {
                    text: 'III. Informacja o przychodach ze sprzedaży tusz zwierzyny płowej i kosztach zagospodarowania obwodu.',
                    fontSize: 11, bold: true
                },
                {
                    table: {
                        widths: ['47%', '5%', '12%', '12%', '12%', '12%'],
                        body: costTable,
                        pageBreak: 'before',
                        dontBreakRows: true
                    }
                },
                {
                    text: 'IV. Dane dotyczące zwierząt łownych.',
                    style: 'planPoint', pageBreak: 'before'
                },
                {
                    text: 'a) zwierzyna gruba',
                    fontSize: 11, bold: true
                },
                {
                    table: {
                        widths: ['14%', '7%', '5%', '6%', '7%', '5%', '6%', '6%', '4%', '4%', '4%', '4%', '7%', '5%', '4%', '4%', '4%', '4%'],
                        body: bigGameTable,
                        pageBreak: 'before',
                        dontBreakRows: true
                    }
                },
                {
                    text: '*- dane niezbędne do prowadzenia gospodarki łowieckiej.',
                    fontSize: 7
                },
                {
                    text: 'b) zwierzyna drobna',
                    fontSize: 11, bold: true, pageBreak: 'before'
                },
                {
                    table: {
                        widths: ['24%', '7%', '7%', '7%', '7%', '6%', '7%', '6%', '7%', '7%', '4%', '4%', '4%', '4%'],
                        body: smallGameTable,
                        pageBreak: 'before',
                        dontBreakRows: true
                    }
                },
                signsAndApprovalsSection
                
            ],
            styles: {
                filledHeader: {
                    color: 'white',
                    fillColor: 'black'
                },
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
                    fontSize: 9,
                    bold: true
                },
                point: {
                    fontSize: 9,
                    alignment: 'left'
                },
                cell: {
                    fontSize: 9,
                    alignment: 'center'
                }
            }
        };

        pdfMake.createPdf(docDefinition).download('RocznyPlan.pdf');
    };
    
    $('#generateAnnualPlanPdf').on('click', function () {
        $.ajax({
            type: "GET",
            url: '/AnnualPlan/GetAnnualPlanJsonData?marketingYearId=' + marketingYearId,
            success: function (data) {
                downloadPdf(data);
            },
            error: function () {
                alert('Coś poszło nie tak, proszę odświeżyć stronę.');
            }
        });
    });
};