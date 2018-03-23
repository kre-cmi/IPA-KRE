import {Component} from '@angular/core';
import { ParameterService} from '../../Services/parameterService';
import {Parameter} from '../parameterEntity';

@Component({
	selector: 'cmi-viaduc-parameterList',
	templateUrl: './parameterList.component.html',
	styleUrls: ['./parameterList.component.less']
})
export class ParameterListComponent {
	public loading: boolean = true;

	public parameters: Parameter[] = [];

	constructor(private _params: ParameterService) {
		this.getAllParameters();
		console.log('done');
	}

	public async getAllParameters() {
		this._params.getAllParameters().then(response => {
			this.parameters = response;
			console.log(this.parameters);
			this.loading = false;
		});
	}

}
