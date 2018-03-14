import { Component } from '@angular/core';
import { ParameterService} from '../Services/parameterService';

@Component({
	selector: 'cmi-viaduc-root',
	templateUrl: './root.component.html',
	styleUrls: ['./root.component.less']
})
export class RootComponent {
	public preloading: boolean = true;

	constructor(private _params: ParameterService) {
		this.preloading = false;
	}

	public getParameters(): void {
		let response = this._params.getAllParameters();
		console.log(response);
	}
}
