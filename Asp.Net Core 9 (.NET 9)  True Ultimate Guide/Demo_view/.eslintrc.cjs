module.exports = {
  env: {
    node: true,
  },
  extends: [
    'eslint:recommended',
    'plugin:vue/vue3-recommended',
  ],
  parserOptions: {
    parser: '@typescript-eslint/parser',
  },
  // plugins: ['only-warn'],
  rules: {
    "no-unused-vars": "warn",
    "no-duplicate-selectors": "off",
    "vue/require-v-for-key": "warn",
    "vue/require-valid-default-prop": "warn",
    "vue/valid-v-for": "off",
    "vue/html-quotes":"off"
  },
  overrides: [
    {
      "files": ["*.js", "*.jsx", "*.ts", "*.vue"],
      "rules": {
        "@typescript-eslint/explicit-function-return-type": "off",
      }
    }
  ]
}