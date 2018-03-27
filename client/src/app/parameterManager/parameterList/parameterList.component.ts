import {Component, EventEmitter} from '@angular/core';
import { ParameterService} from '../../Services/parameterService';
import {Parameter} from '../parameterEntity';

@Component({
	selector: 'cmi-viaduc-parameterList',
	templateUrl: './parameterList.component.html',
	styleUrls: ['./parameterList.component.less']
})
export class ParameterListComponent {
	public loading: boolean = true;
	public filteredParameters: Parameter[] = [];
	private _allParameters: Parameter[] = [];
	public validationEvent: EventEmitter<void> = new EventEmitter<void>();
	public savedSuccessfull: boolean;
	public searchString: string = '';

	constructor(private _params: ParameterService) {
		this.getAllParameters();
	}

	public async getAllParameters() {
		this._params.getAllParameters().then(response => {
			this._allParameters = response;
			this.filteredParameters = this._allParameters;
			this.loading = false;
		});
	}

	public onValueChanged(event: any) {
		this.searchString = event.target.value;
	}

	public emitValidationEvent() {
		this.validationEvent.emit();
	}

	public getClass(): string {
		if (this.savedSuccessfull === true) {
			return 'alert alert-success fade in';
		} else if (this.savedSuccessfull === false) {
			return 'alert alert-danger fade in';
		} else {
			return 'no-display';
		}
	}

	public searchParam() {
		this.filteredParameters = [];
		if (this.searchString !== '') {
			console.log('search string found!');
			this.filteredParameters = this._allParameters.filter((param) =>
				param.name.toLowerCase().indexOf(this.searchString.toLowerCase()) !== -1 || param.value.toLowerCase().indexOf(this.searchString.toLowerCase()) !== -1
			);
		} else {
			console.log('search string not found!');
			this.filteredParameters = this._allParameters;
		}
	}
}
