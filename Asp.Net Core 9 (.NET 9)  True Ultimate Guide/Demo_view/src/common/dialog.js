import { useDialogStore } from 'stores/dialog';
import { shallowRef } from 'vue';


class DialogManger {
  show = (titleVal, contentVal, configVal) => new Promise((resolve) => {
    const isScrollVisible = document.body.scrollHeight > window.innerHeight;
    if (isScrollVisible) {
      document.body.classList.add('!tw-pr-[17px]');
    }
    const item = {
      title: titleVal,
      content: contentVal,
      config: configVal,
      show: true,
      close: resolve,
    };
    const { show } = useDialogStore();
    show(item);
  });

  showContent = (title, content, config) => {
    const component = shallowRef(content);
    config = {
      showFooter: false,
      isComponent: true,
      ...config,
    };
    return this.show(title, component, config);
  };

  showMessage = (title, content, config) => {
    config = {
      showFooter: true,
      buttons: ['ok'],
      isComponent: false,
      isUseI18n: false,
      ...config,
    };
    return this.show(title, content, config);
  };

  showConfirm = (title, content, config) => {
    config = {
      showFooter: true,
      buttons: ['yes', 'no'],
      isComponent: false,
      isUseI18n: true,
      ...config,
    };
    return this.show(title, content, config);
  };

  hide = (value) => {
    const { last, hide } = useDialogStore();
    if (!last) return;
    last.show = false;
    document.body.classList.remove('!tw-pr-[17px]');
    setTimeout(() => {
      last.close(value);
      hide();
    }, 200);
  };
}

export default new DialogManger();
