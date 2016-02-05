module Diving.Controllers {

    import DataService = Diving.Services;

    export class rootController {

        public DiveChild: any;
        private scope: any;

        constructor(private $scope: ng.IScope) {
            this.scope = $scope;   
        }

        public DiveCreateNew() {
            this.scope.DiveChild.CreateNewDive();
        }

        public DiveShowDives() {
            this.scope.DiveChild.CancelCreateNewDive();
        }
    }
}