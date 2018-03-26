import {Component, Input, OnInit} from '@angular/core';
import {Parameter} from '../parameterEntity';
import {ParameterService} from '../../Services/parameterService';
import {Subject} from 'rxjs/Subject';

@Component({
	selector: 'cmi-viaduc-parameter',
	templateUrl: './parameter.component.html',
	styleUrls: ['./parameter.component.less']
})
export class ParameterComponent implements OnInit {
	@Input()
	public parameter: Parameter;

	public active: boolean = false;
	private _newValue: string;
	private _value: string;
	public checked: boolean;
	private static _onChanged: Subject<string> = new Subject();

	constructor (private _paramService: ParameterService) {
	}

	public ngOnInit() {
		ParameterComponent._onChanged.subscribe((name) => {
			if (this.active && this.parameter.name !== name) {
				this.active = false;
			}
		});
	}
	public onValueChanged(event: any) {
		if (this.parameter.type === 'checkbox') {
			this.checked = event.target.checked;
			if (this.checked === (this.parameter.value === 'True')) {
				this.active = false;
			}
		} else {
			this._newValue = event.target.value;
		}
	}

	public onFocus() {
		ParameterComponent._onChanged.next(this.parameter.name);
		this.active = true;
	}

	public saveParameter() {
		console.log('New Value: ' + this._newValue);
		this.parameter.value = this._newValue;
		this._paramService.saveParameter(this.parameter);
	}

	public cancelEdit() {
		this._newValue = null;

		if (this.parameter.type === 'checkbox') {
			this.checked = this.parameter.value === 'True';
		} else {
			this._value = this.parameter.value;
		}
		this.active = false;
	}

	public getValue(): string {
		if (this._value) {
			console.log('GetValueValue: ' + this._value);
			return this._value;
		} else {
			console.log('GetValueParameter: ' + this.parameter.value);
			return this.parameter.value;
		}
	}

	public getChecked(): boolean {
		if (this.checked) {
			return this.checked;
		} else {
			return this.parameter.value === 'True';
		}
	}
}
